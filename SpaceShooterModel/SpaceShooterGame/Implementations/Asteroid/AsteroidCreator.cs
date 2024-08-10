namespace SpaceShooterGame.Implementations.Asteroid
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    internal class AsteroidCreator : EntityCreator
    {
        private readonly IAspectRatioProvider _aspectRatioProvider;
        private readonly AsteroidCreatorSettings _settings;
        private readonly Random _random = new Random(0);
        private readonly float _firstAspectRatio;
        private readonly float _firstCooldown;

        private float _maxAspectRatio;
        private float _cooldown;
        private float _timer;
        private int _count;

        internal AsteroidCreator(IAspectRatioProvider aspectRatioProvider, AsteroidCreatorSettings settings)
        {
            _settings = settings;
            _aspectRatioProvider = aspectRatioProvider;
            _firstAspectRatio = _maxAspectRatio = aspectRatioProvider.AspectRatio;
            _firstCooldown = _cooldown = settings.Cooldown;
            _count = settings.Count;
            _timer = 0f;
            _aspectRatioProvider.AspectRatioChanged += AspectRatio_ValueChanged;
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if (_count == 0) return;

            _timer -= deltaTime;
            while (_timer <= 0)
            {
                _timer += _cooldown;
                _count = _count > 0 ? _count - 1 : _count;

                float size = (_settings.MaxSize - _settings.MinSize) * (float)_random.NextDouble() + _settings.MinSize;
                float halfSize = size * 0.5f;

                float xMin = halfSize - 0.5f * _maxAspectRatio;
                float xMax = 0.5f * _maxAspectRatio - halfSize;
                float x = (xMax - xMin) * (float)_random.NextDouble() + xMin;
                float y = 0.5f + halfSize;
                Vector2 pos = new Vector2(x, y);

                AsteroidSettings asteroidSettings = new AsteroidSettings(_aspectRatioProvider, _settings, pos, size);
                Entity newEntity = new Asteroid(asteroidSettings);
                OnEntityCreated(newEntity);
            }
        }

        private void AspectRatio_ValueChanged()
        {
            if (_aspectRatioProvider.AspectRatio > _maxAspectRatio)
            {
                _maxAspectRatio = _aspectRatioProvider.AspectRatio;
                _cooldown = _firstCooldown * _firstAspectRatio / _maxAspectRatio;
                _timer = MathF.Min(_timer, _cooldown);
            }
        }
    }
}