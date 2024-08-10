namespace SpaceShooterGame.Implementations.PlayerShip
{
    using SpaceShooterGame.Contracts.Internal;

    internal class PlayerShipCreator : EntityCreator
    {
        private readonly IAspectRatioProvider _aspectRatioProvider;
        private readonly PlayerShipCreatorSettings _settings;

        private int _count;

        internal PlayerShipCreator(IAspectRatioProvider aspectRatioProvider, PlayerShipCreatorSettings settings)
        {
            _aspectRatioProvider = aspectRatioProvider;
            _count = settings.Count;
            _settings = settings;
        }

        internal override void AdvanceTime(float deltaTime)
        {
            while (_count > 0)
            {
                _count--;
                PlayerShipSettings playerShipSettings = new PlayerShipSettings(_aspectRatioProvider, _settings);
                Entity newEntity = new PlayerShip(playerShipSettings);
                OnEntityCreated(newEntity);
            }
        }
    }
}