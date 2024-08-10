namespace PresenterActivator
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    internal class PlayerShipPresenter : EntityPresenter, IPlayerShip
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly ResourceRequest _spriteRequest;

        public PlayerShipPresenter(GamePresenter gamePresenter, IPresentable presentableEntity, ViewableEntity viewableEntity) : base(gamePresenter, presentableEntity, viewableEntity)
        {
            _spriteRenderer = ViewableEntity.AddComponent<SpriteRenderer>();
            _spriteRequest = Resources.LoadAsync<Sprite>("player-ship-1");

            _spriteRequest.completed += SpriteRequest_Completed;
            ViewableEntity.Updating += ViewableEntity_Updating;
        }

        public void SetDestination(float x, float y)
        {
            (PresentableEntity as IPlayerShip)?.SetDestination(x, y);
        }

        private void SpriteRequest_Completed(AsyncOperation asyncOperation)
        {
            _spriteRenderer.sprite = _spriteRequest.asset as Sprite;
        }

        private void ViewableEntity_Updating()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mouseViewportPos = GamePresenter.ScreenToViewportPoint(Input.mousePosition);
                SetDestination(mouseViewportPos.x, mouseViewportPos.y);
            }
        }
    }
}