namespace SpaceShooterGame.Implementations.PlayerShip
{
    using SpaceShooterGame.Contracts.Internal;

    internal class PlayerShipCreator : EntityCreator
    {
        private readonly IScreenHeightProvider _screenHeightProvider;
        private readonly IAspectRatioProvider _aspectRatioProvider;
        private readonly PlayerShipCreatorSettings _settings;

        private int _count;

        internal PlayerShipCreator(IAspectRatioProvider aspectRatioProvider, IScreenHeightProvider screenHeightProvider, PlayerShipCreatorSettings settings)
        {
            _screenHeightProvider = screenHeightProvider;
            _aspectRatioProvider = aspectRatioProvider;
            _count = settings.Count;
            _settings = settings;
        }

        internal override void AdvanceTime(float deltaTime)
        {
            while (_count > 0)
            {
                _count--;
                PlayerShipSettings playerShipSettings = new PlayerShipSettings(_aspectRatioProvider, _screenHeightProvider, _settings);
                Entity newEntity = new PlayerShip(playerShipSettings);
                OnEntityCreated(newEntity);
            }
        }
    }
}