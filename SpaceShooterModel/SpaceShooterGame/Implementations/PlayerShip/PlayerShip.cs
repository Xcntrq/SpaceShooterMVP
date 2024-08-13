namespace SpaceShooterGame.Implementations.PlayerShip
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;

    internal class PlayerShip : Entity, IPresentable, IPlayerShip, IVariablePosition, IConstantSize, IPhysical, IDestroyable
    {
        private readonly IViewportConnection _viewportConnection;
        private readonly float _speed;
        private readonly float _size;

        private Vector2 _currentPos;
        private Vector2 _targetPos;

        internal PlayerShip(PlayerShipSettings settings)
        {
            _size = settings.Size;
            _speed = settings.Speed;
            _viewportConnection = settings.ViewportConnection;
            _currentPos = _viewportConnection.ViewportToGame(settings.X, settings.Y);
            _targetPos = _currentPos = ClampWithAspectRatio(_currentPos, _viewportConnection.AspectRatio);
            _viewportConnection.ScreenHeightChanged += ViewportConnection_ScreenHeightChanged;
            _viewportConnection.AspectRatioChanged += ViewportConnection_AspectRatioChanged;
        }

        public event Action? PositionChanged;
        public event Action? Destroyed;

        internal override event Action? Destroying;

        public string Name => GetType().Name;
        public (float, float) Position => _viewportConnection.GameToViewport(_currentPos);
        public float Size => _size;

        public void SetDestination(float x, float y)
        {
            _targetPos = _viewportConnection.ViewportToGame(x, y);
            _targetPos = ClampWithAspectRatio(_targetPos, _viewportConnection.AspectRatio);
        }

        public void ProcessCollision(IPhysical anotherPhysical)
        {
            if (anotherPhysical is IAsteroid)
                DestroySelf();
        }

        internal override void AdvanceTime(float deltaTime)
        {
            if ((deltaTime == 0) || (_currentPos == _targetPos)) return;

            Vector2 dir = Vector2.Normalize(_targetPos - _currentPos);
            Vector2 newPos = _currentPos + _speed * deltaTime * dir;

            float distWanted = Vector2.DistanceSquared(_currentPos, newPos);
            float distNeeded = Vector2.DistanceSquared(_currentPos, _targetPos);

            _currentPos = distNeeded < distWanted ? _targetPos : newPos;
            PositionChanged?.Invoke();
        }

        private void ViewportConnection_ScreenHeightChanged(float value)
        {
            _targetPos *= value;
            _currentPos *= value;
            PositionChanged?.Invoke();
        }

        private void ViewportConnection_AspectRatioChanged(float aspectRatio)
        {
            _currentPos = ClampWithAspectRatio(_currentPos, aspectRatio);
            _targetPos = ClampWithAspectRatio(_targetPos, aspectRatio);
            PositionChanged?.Invoke();
        }

        private Vector2 ClampWithAspectRatio(Vector2 v, float aspectRatio)
        {
            float halfSize = _size * 0.5f;
            float halfAspect = aspectRatio * 0.5f;
            float x = Math.Clamp(v.X, halfSize - halfAspect, halfAspect - halfSize);
            float y = Math.Clamp(v.Y, halfSize - 0.5f, 0.5f - halfSize);
            return new Vector2(x, y);
        }

        private void DestroySelf()
        {
            Destroying?.Invoke();
            _viewportConnection.ScreenHeightChanged -= ViewportConnection_ScreenHeightChanged;
            _viewportConnection.AspectRatioChanged -= ViewportConnection_AspectRatioChanged;
            Destroyed?.Invoke();
        }
    }
}