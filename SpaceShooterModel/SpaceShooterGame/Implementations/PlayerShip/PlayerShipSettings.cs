namespace SpaceShooterGame.Implementations.PlayerShip
{
    using SpaceShooterGame.Contracts.Internal;

    internal struct PlayerShipSettings
    {
        internal PlayerShipSettings(IViewportConnection viewportConnection, PlayerShipCreatorSettings playerShipCreatorSettings)
        {
            X = playerShipCreatorSettings.X;
            Y = playerShipCreatorSettings.Y;
            Size = playerShipCreatorSettings.Size;
            Speed = playerShipCreatorSettings.Speed;
            ViewportConnection = viewportConnection;
        }

        internal float X { get; private set; }
        internal float Y { get; private set; }
        internal float Size { get; private set; }
        internal float Speed { get; private set; }
        internal IViewportConnection ViewportConnection { get; private set; }
    }
}