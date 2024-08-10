namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    internal class AsteroidPresenter : EntityPresenter, IAsteroid
    {
        private ResourceRequest _spriteRequest;

        internal override void Initialize(GamePresenter _, IPresentable __)
        {
            _spriteRequest = Resources.LoadAsync<Sprite>("asteroid-1");
            _spriteRequest.completed += SpriteRequest_Completed;
        }

        private void SpriteRequest_Completed(AsyncOperation asyncOperation)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteRequest.asset as Sprite;
        }
    }
}