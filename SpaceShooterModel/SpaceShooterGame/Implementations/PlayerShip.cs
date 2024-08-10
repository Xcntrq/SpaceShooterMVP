namespace SpaceShooterGame
{
    using System;
    using System.Numerics;

    internal class PlayerShip : Entity, IPresentableEntity, IPlayerShip, ITrackablePosition, ITrackableSize
    {
        private readonly IAspectRatio _aspectRatio;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;
        private Vector2 _targetPos;

        internal PlayerShip(PlayerShipSettings playerShipSettings, IAspectRatio aspectRatio)
        {
            _aspectRatio = aspectRatio;
            _size = playerShipSettings.Size;
            _speed = playerShipSettings.Speed;
            _currentPos.X = (playerShipSettings.StartPosX - 0.5f) * _aspectRatio.Value;
            _currentPos.Y = playerShipSettings.StartPosY;
            _currentPos = ClampToAspectRatio(_currentPos);
            _targetPos = _currentPos;

            _aspectRatio.ValueChanged += AspectRatio_ValueChanged;

            // preventing a warning
            SizeChanged?.Invoke();
        }

        public event Action? PositionChanged;
        public event Action? SizeChanged;

        public string Name => "PlayerShip";
        public float X => (_currentPos.X / _aspectRatio.Value) + 0.5f;
        public float Y => _currentPos.Y;
        public float Size => _size;

        public void SetDestination(float x, float y)
        {
            float newX = (x - 0.5f) * _aspectRatio.Value;
            _targetPos = new Vector2(newX, y);
            _targetPos = ClampToAspectRatio(_targetPos);
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if ((deltaTime == 0) || (_currentPos == _targetPos)) return;

            Vector2 dir = Vector2.Normalize(_targetPos - _currentPos);
            Vector2 newPos = _currentPos + (_speed * deltaTime * dir);

            float distWanted = Vector2.DistanceSquared(_currentPos, newPos);
            float distNeeded = Vector2.DistanceSquared(_currentPos, _targetPos);

            _currentPos = (distNeeded < distWanted) ? _targetPos : newPos;
            PositionChanged?.Invoke();
        }

        private void AspectRatio_ValueChanged()
        {
            _currentPos = ClampToAspectRatio(_currentPos);
            _targetPos = ClampToAspectRatio(_targetPos);
            PositionChanged?.Invoke();
        }

        private Vector2 ClampToAspectRatio(Vector2 v)
        {
            float halfSize = _size * 0.5f;
            float halfAspect = _aspectRatio.Value * 0.5f;
            float x = Math.Clamp(v.X, halfSize - halfAspect, halfAspect - halfSize);
            float y = Math.Clamp(v.Y, halfSize, 1f - halfSize);
            return new Vector2(x, y);
        }
    }
}