namespace PresenterActivator
{
    using System;
    using SpaceShooterGame;
    using UnityEngine;

    internal class TrackablePositionPresenter : EntityPresenter, ITrackablePosition
    {
        private readonly ITrackablePosition _trackablePosition;

        public TrackablePositionPresenter(GamePresenter gamePresenter, IPresentableEntity presentableEntity, ViewableEntity viewableEntity) : base(gamePresenter, presentableEntity, viewableEntity)
        {
            _trackablePosition = PresentableEntity as ITrackablePosition;

            TrackablePosition_PositionChanged();
            _trackablePosition.PositionChanged += TrackablePosition_PositionChanged;
        }

        public event Action PositionChanged;

        public float X => _trackablePosition.X;
        public float Y => _trackablePosition.Y;

        private void TrackablePosition_PositionChanged()
        {
            Vector2 newPos = new(X, Y);
            newPos = GamePresenter.ViewportToWorldPoint(newPos);
            ViewableEntity.Position = newPos;
            PositionChanged?.Invoke();
        }
    }
}