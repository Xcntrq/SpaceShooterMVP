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
            int number = Random.Range(1, 5);
            _spriteRequest = Resources.LoadAsync<Sprite>($"asteroid-{number}");
            _spriteRequest.completed += SpriteRequest_Completed;
        }

        private void SpriteRequest_Completed(AsyncOperation asyncOperation)
        {
            if (this != null)
            {
                GetComponent<SpriteRenderer>().sprite = _spriteRequest.asset as Sprite;
            }
        }
    }
}