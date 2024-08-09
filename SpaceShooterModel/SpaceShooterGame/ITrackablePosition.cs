namespace SpaceShooterGame
{
    using System;

    public interface ITrackablePosition
    {
        public event Action PositionChanged;

        public float X { get; }
        public float Y { get; }
    }
}