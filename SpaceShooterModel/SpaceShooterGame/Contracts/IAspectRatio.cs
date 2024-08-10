namespace SpaceShooterGame
{
    using System;

    internal interface IAspectRatio
    {
        public event Action ValueChanged;

        public float Value { get; }
    }
}