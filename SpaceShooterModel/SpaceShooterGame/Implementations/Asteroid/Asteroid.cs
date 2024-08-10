namespace SpaceShooterGame.Implementations.Asteroid
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;

    internal class Asteroid : Entity, IPresentable, IAsteroid, IVariablePosition, IConstantSize
    {
        private readonly IAspectRatioProvider _aspectRatioProvider;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;

        internal Asteroid(AsteroidSettings settings)
        {
            _aspectRatioProvider = settings.AspectRatioProvider;
            _currentPos = settings.Position;
            _speed = settings.Speed;
            _size = settings.Size;
        }

        public event Action? PositionChanged;

        public string Name => GetType().ToString();
        public (float, float) Position => Game.GameToViewport(_currentPos, _aspectRatioProvider);
        public float Size => _size;

        internal override void AdvanceTime(float deltaTime)
        {
            if (deltaTime == 0) return;

            _currentPos -= deltaTime * _speed * Vector2.UnitY;
            PositionChanged?.Invoke();
        }
    }
}