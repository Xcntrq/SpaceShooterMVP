namespace PresenterMoBe
{
    using System.Collections.Generic;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(SpriteRenderer))]
    internal class PlayerShipPresenter : EntityPresenter, IPlayerShip
    {
        private static int _number = 0;

        private IPlayerShip _playerShip;
        private GamePresenter _gamePresenter;
        private ResourceRequest _spriteRequest;

        public void SetDestination(float x, float y)
        {
            _playerShip?.SetDestination(x, y);
        }

        internal override void Initialize(GamePresenter gamePresenter, IPresentable presentableEntity)
        {
            _gamePresenter = gamePresenter;
            _playerShip = presentableEntity as IPlayerShip;

            _number++;
            _spriteRequest = Resources.LoadAsync<Sprite>($"player-ship-{_number}");
            _spriteRequest.completed += SpriteRequest_Completed;
        }

        private void SpriteRequest_Completed(AsyncOperation asyncOperation)
        {
            if (this != null)
            {
                GetComponent<SpriteRenderer>().sprite = _spriteRequest.asset as Sprite;
            }
        }

        private void OnDestroy()
        {
            _number--;
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