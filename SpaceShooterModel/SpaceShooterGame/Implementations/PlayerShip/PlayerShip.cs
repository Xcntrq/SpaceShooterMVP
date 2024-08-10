namespace SpaceShooterGame.Implementations.PlayerShip
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;

    internal class PlayerShip : Entity, IPresentable, IPlayerShip, IVariablePosition, IConstantSize
    {
        private readonly IAspectRatioProvider _aspectRatioProvider;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;
        private Vector2 _targetPos;

        internal PlayerShip(PlayerShipSettings settings)
        {
            _aspectRatioProvider = settings.AspectRatioProvider;
            _speed = settings.Speed;
            _size = settings.Size;
            _currentPos = Game.ViewportToGame(settings.X, settings.Y, _aspectRatioProvider);
            _targetPos = _currentPos = ClampWithAspectRatio(_currentPos);
            _aspectRatioProvider.AspectRatioChanged += AspectRatio_ValueChanged;
        }

        public event Action? PositionChanged;

        public string Name => GetType().ToString();
        public (float, float) Position => Game.GameToViewport(_currentPos, _aspectRatioProvider);
        public float Size => _size;

        public void SetDestination(float x, float y)
        {
            _targetPos = Game.ViewportToGame(x, y, _aspectRatioProvider);
            _targetPos = ClampWithAspectRatio(_targetPos);
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if (deltaTime == 0 || _currentPos == _targetPos) return;

            Vector2 dir = Vector2.Normalize(_targetPos - _currentPos);
            Vector2 newPos = _currentPos + _speed * deltaTime * dir;

            float distWanted = Vector2.DistanceSquared(_currentPos, newPos);
            float distNeeded = Vector2.DistanceSquared(_currentPos, _targetPos);

            _currentPos = distNeeded < distWanted ? _targetPos : newPos;
            PositionChanged?.Invoke();
        }

        private void AspectRatio_ValueChanged()
        {
            _currentPos = ClampWithAspectRatio(_currentPos);
            _targetPos = ClampWithAspectRatio(_targetPos);
            PositionChanged?.Invoke();
        }

        private Vector2 ClampWithAspectRatio(Vector2 v)
        {
            float halfSize = _size * 0.5f;
            float halfAspect = _aspectRatioProvider.AspectRatio * 0.5f;
            float x = Math.Clamp(v.X, halfSize - halfAspect, halfAspect - halfSize);
            float y = Math.Clamp(v.Y, halfSize - 0.5f, 0.5f - halfSize);
            return new Vector2(x, y);
        }
    }
}