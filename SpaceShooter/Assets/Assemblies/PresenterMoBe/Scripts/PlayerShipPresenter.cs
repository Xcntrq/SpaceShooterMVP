namespace PresenterMoBe
{
    using System;
    using System.Collections.Generic;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(SpriteRenderer))]
    internal class PlayerShipPresenter : EntityPresenter, IPlayerShip
    {
        private static readonly Color[] _colors = new Color[2] { Color.cyan, Color.red };

        private static int _number = 0;

        private IPlayerShip _playerShip;
        private GamePresenter _gamePresenter;
        private ResourceRequest _spriteRequest;
        private ResourceRequest _effectRequest;
        private ParticleSystem _effectSystem;
        private int _cachedNumber;

        public event Action<bool> Destroyed;

        public void SetDestination(float x, float y)
        {
            _playerShip?.SetDestination(x, y);
        }

        internal override void Initialize(GamePresenter gamePresenter, IPresentable presentableEntity)
        {
            _gamePresenter = gamePresenter;
            _playerShip = presentableEntity as IPlayerShip;
            _playerShip.Destroyed += PlayerShip_Destroyed;

            _cachedNumber = ++_number;
            _spriteRequest = Resources.LoadAsync<Sprite>($"player-ship-{_cachedNumber}");
            _effectRequest = Resources.LoadAsync<ParticleSystem>("player-boom");
            _spriteRequest.completed += SpriteRequest_Completed;
            _effectRequest.completed += EffectRequest_Completed;
        }

        private void SpriteRequest_Completed(AsyncOperation _)
        {
            if (this != null)
            {
                GetComponent<SpriteRenderer>().sprite = _spriteRequest.asset as Sprite;
            }
        }

        private void EffectRequest_Completed(AsyncOperation _)
        {
            if (this != null)
            {
                _effectSystem = _effectRequest.asset as ParticleSystem;
            }
        }

        private void PlayerShip_Destroyed(bool hasEffect)
        {
            if (hasEffect && (_effectSystem != null))
            {
                ParticleSystem particleSystem = Instantiate(_effectSystem, gameObject.transform.position, gameObject.transform.rotation);
                ParticleSystem[] ps = particleSystem.GetComponentsInChildren<ParticleSystem>(true);
                foreach (ParticleSystem p in ps)
                {
                    ParticleSystem.MainModule main = p.main;
                    main.startColor = _colors[_cachedNumber - 1];
                    p.transform.localScale *= 0.01f;
                }
            }

            Destroyed?.Invoke(false);
        }

        private void OnDestroy()
        {
            _number--;
            _playerShip.Destroyed -= PlayerShip_Destroyed;
            _spriteRequest.completed -= SpriteRequest_Completed;
            _effectRequest.completed -= EffectRequest_Completed;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                ProcessTouch();
            }

            if ((Input.touchCount == 0) && Input.GetMouseButton(0))
            {
                ProcessMouse();
            }
        }

        private void ProcessTouch()
        {
            foreach (Touch touch in Input.touches)
            {
                if (IsPointerOverUIObject(touch.position)) return;
            }

            foreach (Touch touch in Input.touches)
            {
                if (_gamePresenter.IsPixelInViewport(touch.position))
                {
                    Vector2 mouseViewportPos = _gamePresenter.ScreenToViewportPoint(touch.position);
                    SetDestination(mouseViewportPos.x, mouseViewportPos.y);
                }
            }
        }

        private void ProcessMouse()
        {
            if (IsPointerOverUIObject(Input.mousePosition)) return;

            Vector2 mouseViewportPos = _gamePresenter.ScreenToViewportPoint(Input.mousePosition);
            SetDestination(mouseViewportPos.x, mouseViewportPos.y);
        }

        private bool IsPointerOverUIObject(Vector2 position)
        {
            PointerEventData eventDataCurrentPosition = new(EventSystem.current)
            {
                position = position,
            };

            List<RaycastResult> results = new();
            GraphicRaycaster uiRaycaster = _gamePresenter.GameplayCanvas.GetComponent<GraphicRaycaster>();
            uiRaycaster.Raycast(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}