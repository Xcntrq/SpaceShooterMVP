namespace SpaceShooterGame.Contracts.Internal
{
    using System;

    internal interface IScreenHeightProvider
    {
        public event Action<float> ScreenHeightChanged;
    }
}