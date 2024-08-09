namespace PresenterMoBe
{
    using System;
    using SpaceShooterGame;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    internal class TrackablePositionPresenter : EntityPresenter, ITrackablePosition
    {
        private GamePresenter _gamePresenter;
        private ITrackablePosition _trackablePosition;

        public event Action PositionChanged;

        public float X => _trackablePosition.X;
        public float Y => _trackablePosition.Y;

        internal override void Initialize(GamePresenter gamePresenter, IPresentableEntity presentableEntity)
        {
            _gamePresenter = gamePresenter;
            _trackablePosition = presentableEntity as ITrackablePosition;

            TrackablePosition_PositionChanged();
            _trackablePosition.PositionChanged += TrackablePosition_PositionChanged;
        }

        private void TrackablePosition_PositionChanged()
        {
            Vector2 newPos = new(X, Y);
            newPos = _gamePresenter.ViewportToWorldPoint(newPos);
            transform.localPosition = newPos;
            PositionChanged?.Invoke();
        }

        private void OnDestroy()
        {
            if (_trackablePosition != null)
            {
                _trackablePosition.PositionChanged -= TrackablePosition_PositionChanged;
            }
        }
    }
}