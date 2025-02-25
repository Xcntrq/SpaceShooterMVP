namespace SpaceShooterGame.Implementations.Asteroid
{
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    internal struct AsteroidSettings
    {
        internal AsteroidSettings(IViewportConnection viewportConnection, Vector2 position, float rotation, float rotSpeed, float speed, float size)
        {
            ViewportConnection = viewportConnection;
            Position = position;
            Rotation = rotation;
            RotSpeed = rotSpeed;
            Speed = speed;
            Size = size;
        }

        internal IViewportConnection ViewportConnection { get; private set; }
        internal Vector2 Position { get; private set; }
        internal float Rotation { get; private set; }
        internal float RotSpeed { get; private set; }
        internal float Speed { get; private set; }
        internal float Size { get; private set; }
    }
}