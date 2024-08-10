namespace SpaceShooterGame.Implementations.Asteroid
{
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    internal struct AsteroidSettings
    {
        internal AsteroidSettings(IAspectRatioProvider aspectRatioProvider, AsteroidCreatorSettings asteroidCreatorSettings, Vector2 position, float size)
        {
            Speed = asteroidCreatorSettings.Speed;
            AspectRatioProvider = aspectRatioProvider;
            Position = position;
            Size = size;
        }

        internal IAspectRatioProvider AspectRatioProvider { get; private set; }
        internal Vector2 Position { get; private set; }
        internal float Speed { get; private set; }
        internal float Size { get; private set; }
    }
}