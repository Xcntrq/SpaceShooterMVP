namespace SpaceShooterGame.Implementations.PlayerShip
{
    using SpaceShooterGame.Contracts.Internal;

    internal struct PlayerShipSettings
    {
        internal PlayerShipSettings(IAspectRatioProvider aspectRatioProvider, PlayerShipCreatorSettings playerShipCreatorSettings)
        {
            AspectRatioProvider = aspectRatioProvider;
            X = playerShipCreatorSettings.X;
            Y = playerShipCreatorSettings.Y;
            Size = playerShipCreatorSettings.Size;
            Speed = playerShipCreatorSettings.Speed;
        }

        internal float X { get; private set; }
        internal float Y { get; private set; }
        internal float Size { get; private set; }
        internal float Speed { get; private set; }
        internal IAspectRatioProvider AspectRatioProvider { get; private set; }
    }
}