namespace SpaceShooterGame.Implementations.Main
{
    using System;
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    internal class ViewportConnection : IViewportConnection
    {
        private int? _lastScreenHeigh;
        private float _aspectRatio;

        internal ViewportConnection(float aspectRatio)
        {
            _aspectRatio = aspectRatio;
        }

        public event Action<float>? AspectRatioChanged;
        public event Action<float>? ScreenHeightChanged;

        public float AspectRatio => _aspectRatio;

        public (float, float) GameToViewport(Vector2 vector)
        {
            float x = vector.X / _aspectRatio + 0.5f;
            float y = vector.Y + 0.5f;
            return (x, y);
        }

        public Vector2 ViewportToGame(float x, float y)
        {
            x = (x - 0.5f) * _aspectRatio;
            return new Vector2(x, y - 0.5f);
        }

        internal void SetScreenHeight(int screenHeigh)
        {
            _lastScreenHeigh ??= screenHeigh;
            float sh = (float)_lastScreenHeigh / screenHeigh;
            ScreenHeightChanged?.Invoke(sh);
            _lastScreenHeigh = screenHeigh;
        }

        internal void SetAspectRatio(float aspectRatio)
        {
            _aspectRatio = aspectRatio;
            AspectRatioChanged?.Invoke(aspectRatio);
        }
    }
}