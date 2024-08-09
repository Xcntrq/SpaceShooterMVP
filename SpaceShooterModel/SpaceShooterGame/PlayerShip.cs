namespace SpaceShooterGame
{
    using System;
    using System.Numerics;

    internal class PlayerShip : Entity, IPresentableEntity, IPlayerShip, ITrackablePosition
    {
        private readonly float _speed;

        private Vector2 _currentPos;
        private Vector2 _targetPos;

        internal PlayerShip(PlayerShipSettings playerShipSettings)
        {
            _speed = playerShipSettings.Speed;
            _currentPos.X = playerShipSettings.StartPosX;
            _currentPos.Y = playerShipSettings.StartPosY;
            _targetPos = _currentPos;
        }

        public event Action? PositionChanged;

        public string Name => "PlayerShip";
        public float X => _currentPos.X;
        public float Y => _currentPos.Y;

        public void SetDestination(float x, float y)
        {
            _targetPos.X = x;
            _targetPos.Y = y;
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
    }
}