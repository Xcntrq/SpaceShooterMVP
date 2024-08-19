namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [RequireComponent(typeof(SpriteRenderer))]
    internal class AsteroidPresenter : EntityPresenter, IAsteroid
    {
        private IAsteroid _asteroid;
        private ResourceRequest _spriteRequest;
        private ResourceRequest _effectRequest;
        private ParticleSystem _effectSystem;

        public event System.Action<bool> Destroyed;

        internal override void Initialize(GamePresenter _, IPresentable presentable)
        {
            _asteroid = presentable as IAsteroid;
            _asteroid.Destroyed += Asteroid_Destroyed;
            int number = Random.Range(1, 5);
            _spriteRequest = Resources.LoadAsync<Sprite>($"asteroid-{number}");
            _effectRequest = Resources.LoadAsync<ParticleSystem>("asteroid-boom");
            _spriteRequest.completed += SpriteRequest_Completed;
            _effectRequest.completed += EffectRequest_Completed;
        }

        private void Asteroid_Destroyed(bool hasEffect)
        {
            if (hasEffect && (_effectSystem != null))
            {
                Instantiate(_effectSystem, gameObject.transform.position, gameObject.transform.rotation);
            }

            Destroyed?.Invoke(false);
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

        private void OnDestroy()
        {
            _asteroid.Destroyed -= Asteroid_Destroyed;
            _spriteRequest.completed -= SpriteRequest_Completed;
            _effectRequest.completed -= EffectRequest_Completed;
        }
    }
}