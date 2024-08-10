namespace SpaceShooterGame.Implementations.Main
{
    using System;
    using SpaceShooterGame.Contracts.Internal;

    internal class AspectRatioProvider : IAspectRatioProvider
    {
        private float _aspectRatio;

        internal AspectRatioProvider(float aspectRatio)
        {
            _aspectRatio = aspectRatio;
        }

        public event Action? AspectRatioChanged;

        public float AspectRatio => _aspectRatio;

        internal void SetValue(float aspectRatio)
        {
            _aspectRatio = aspectRatio;
            AspectRatioChanged?.Invoke();
        }
    }
}