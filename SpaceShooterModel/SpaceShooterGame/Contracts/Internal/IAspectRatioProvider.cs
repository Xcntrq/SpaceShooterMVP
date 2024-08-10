namespace SpaceShooterGame.Contracts.Internal
{
    using System;

    internal interface IAspectRatioProvider
    {
        public event Action AspectRatioChanged;

        public float AspectRatio { get; }
    }
}