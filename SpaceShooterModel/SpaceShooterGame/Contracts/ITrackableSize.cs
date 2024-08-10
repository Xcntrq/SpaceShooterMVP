namespace SpaceShooterGame
{
    using System;

    public interface ITrackableSize
    {
        public event Action SizeChanged;

        public float Size { get; }
    }
}