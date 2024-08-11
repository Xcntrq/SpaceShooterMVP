namespace SpaceShooterGame.Implementations.Main
{
    using System;
    using SpaceShooterGame.Contracts.Internal;

    internal class ScreenHeightProvider : IScreenHeightProvider
    {
        private int? _lastScreenHeigh;

        public event Action<float>? ScreenHeightChanged;

        internal void SetValue(int screenHeigh)
        {
            _lastScreenHeigh ??= screenHeigh;
            float sh = (float)_lastScreenHeigh / screenHeigh;
            ScreenHeightChanged?.Invoke(sh);
            _lastScreenHeigh = screenHeigh;
        }
    }
}