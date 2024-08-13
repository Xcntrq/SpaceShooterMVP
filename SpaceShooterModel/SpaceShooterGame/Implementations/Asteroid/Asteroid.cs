namespace SpaceShooterGame.Implementations.Asteroid
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;

    internal class Asteroid : Entity, IPresentable, IAsteroid, IVariablePosition, IConstantSize, IDestroyable, IPhysical
    {
        private readonly IViewportConnection _viewportConnection;
        private readonly float _targetY;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;

        internal Asteroid(AsteroidSettings settings)
        {
            _viewportConnection = settings.ViewportConnection;
            _targetY = -0.5f - 0.5f * settings.Size;
            _currentPos = settings.Position;
            _speed = settings.Speed;
            _size = settings.Size;
        }

        public event Action? PositionChanged;
        public event Action? Destroyed;

        internal override event Action? Destroying;

        public string Name => GetType().Name;
        public (float, float) Position => _viewportConnection.GameToViewport(_currentPos);
        public float Size => _size;

        public void ProcessCollision(IPhysical anotherPhysical)
        {
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if ((deltaTime == 0) && (_currentPos.Y >= _targetY)) return;

            _currentPos -= deltaTime * _speed * Vector2.UnitY;
            PositionChanged?.Invoke();

            if (_currentPos.Y < _targetY)
                DestroySelf();
        }

        private void DestroySelf()
        {
            Destroying?.Invoke();
            Destroyed?.Invoke();
        }
    }
}