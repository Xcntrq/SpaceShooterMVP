namespace SpaceShooterGame.Implementations.Asteroid
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;

    internal class Asteroid : Entity, IPresentable, IAsteroid, IVariablePosition, IConstantSize, IDestroyable, IPhysical, IVariableRotation
    {
        private readonly IViewportConnection _viewportConnection;
        private readonly float _rotSpeed;
        private readonly float _targetY;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;
        private float _rotation;

        internal Asteroid(AsteroidSettings settings)
        {
            _viewportConnection = settings.ViewportConnection;
            _targetY = -0.5f - 0.5f * settings.Size;
            _currentPos = settings.Position;
            _rotation = settings.Rotation;
            _rotSpeed = settings.RotSpeed;
            _speed = settings.Speed;
            _size = settings.Size;
        }

        public event Action? PositionChanged;
        public event Action? RotationChanged;
        public event Action<bool>? Destroyed;

        internal override event Action? Destroying;

        public string Name => GetType().Name;
        public (float, float) Position => _viewportConnection.GameToViewport(_currentPos);
        public float Rotation => _rotation;
        public float Size => _size;

        public void ProcessCollision(IPhysical anotherPhysical)
        {
            if (anotherPhysical is IAsteroid && anotherPhysical is IVariablePosition position)
            {
                if (position.Position.Item2 < Position.Item2)
                    DestroySelf(false);
            }
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if ((deltaTime == 0) && (_currentPos.Y >= _targetY)) return;

            _rotation += deltaTime * _rotSpeed;
            RotationChanged?.Invoke();

            _currentPos -= deltaTime * _speed * Vector2.UnitY;
            PositionChanged?.Invoke();

            if (_currentPos.Y < _targetY)
                DestroySelf(false);
        }

        private void DestroySelf(bool hasEffect)
        {
            Destroying?.Invoke();
            Destroyed?.Invoke(hasEffect);
        }
    }
}