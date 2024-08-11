namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    internal class PlayerShipPresenter : EntityPresenter, IPlayerShip
    {
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

            _spriteRequest = Resources.LoadAsync<Sprite>("player-ship-1");
            _spriteRequest.completed += SpriteRequest_Completed;
        }

        private void SpriteRequest_Completed(AsyncOperation asyncOperation)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteRequest.asset as Sprite;
        }

        private void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                if (_gamePresenter.IsPixelInViewport(touch.position))
                {
                    Vector2 mouseViewportPos = _gamePresenter.ScreenToViewportPoint(touch.position);
                    SetDestination(mouseViewportPos.x, mouseViewportPos.y);
                }
            }

            if ((Input.touchCount == 0) && Input.GetMouseButton(0))
            {
                Vector2 mouseViewportPos = _gamePresenter.ScreenToViewportPoint(Input.mousePosition);
                SetDestination(mouseViewportPos.x, mouseViewportPos.y);
            }
        }
    }
}