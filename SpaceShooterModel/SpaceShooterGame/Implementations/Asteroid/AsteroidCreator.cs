namespace SpaceShooterGame.Implementations.Asteroid
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    internal class AsteroidCreator : EntityCreator
    {
        private readonly IViewportConnection _viewportConnection;
        private readonly AsteroidCreatorSettings _settings;
        private readonly Random _random = new Random(0);
        private readonly float _firstAspectRatio;
        private readonly float _firstCooldown;

        private float _maxAspectRatio;
        private float _cooldown;
        private float _timer;
        private int _count;

        internal AsteroidCreator(IViewportConnection viewportConnection, AsteroidCreatorSettings settings)
        {
            _firstAspectRatio = _maxAspectRatio = viewportConnection.AspectRatio;
            _firstCooldown = _cooldown = settings.Cooldown;
            _viewportConnection = viewportConnection;
            _count = settings.Count;
            _settings = settings;
            _timer = 0f;
            _viewportConnection.AspectRatioChanged += ViewportConnection_AspectRatioChanged;
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

                AsteroidSettings asteroidSettings = new AsteroidSettings(_viewportConnection, pos, _settings.Speed, size);
                Entity newEntity = new Asteroid(asteroidSettings);
                OnEntityCreated(newEntity);
            }
        }

        private void ViewportConnection_AspectRatioChanged(float aspectRatio)
        {
            if (aspectRatio > _maxAspectRatio)
            {
                _maxAspectRatio = aspectRatio;
                _cooldown = _firstCooldown * _firstAspectRatio / _maxAspectRatio;
                _timer = MathF.Min(_timer, _cooldown);
            }
        }
    }
}