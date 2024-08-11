namespace SpaceShooterGame.Contracts.Internal
{
    using System;
    using System.Numerics;

    internal interface IViewportConnection
    {
        public event Action<float> AspectRatioChanged;
        public event Action<float> ScreenHeightChanged;

        public float AspectRatio { get; }

        public (float, float) GameToViewport(Vector2 vector);
        public Vector2 ViewportToGame(float x, float y);
    }
}