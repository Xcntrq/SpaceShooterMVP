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
            _firstCooldown = _cooldown = settings.Cooldown / viewportConnection.AspectRatio;
            _firstAspectRatio = _maxAspectRatio = viewportConnection.AspectRatio;
            _viewportConnection = viewportConnection;
            _count = settings.Count;
            _settings = settings;
            _timer = 0f;
            _viewportConnection.AspectRatioChanged += ViewportConnection_AspectRatioChanged;
        }

        internal override event Action<Entity>? EntityCreated;
        internal override event Action? Destroying;

        internal override void AdvanceTime(float deltaTime)
        {
            if (_count == 0)
            {
                DestroySelf();
                return;
            }

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

                float rot = 360f * (float)_random.NextDouble();
                float rotSpeed = 0.2f * 360f * ((float)_random.NextDouble() - 0.5f);

                AsteroidSettings asteroidSettings = new AsteroidSettings(_viewportConnection, pos, rot, rotSpeed, _settings.Speed, size);
                Entity newEntity = new Asteroid(asteroidSettings);
                EntityCreated?.Invoke(newEntity);
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

        private void DestroySelf()
        {
            Destroying?.Invoke();
            _viewportConnection.AspectRatioChanged -= ViewportConnection_AspectRatioChanged;
        }
    }
}