namespace SpaceShooterGame.Implementations.PlayerShip
{
    using SpaceShooterGame.Contracts.Internal;

    internal class PlayerShipCreator : EntityCreator
    {
        private readonly IViewportConnection _viewportConnection;
        private readonly PlayerShipCreatorSettings _settings;

        private int _count;

        internal PlayerShipCreator(IViewportConnection viewportConnection, PlayerShipCreatorSettings settings)
        {
            _viewportConnection = viewportConnection;
            _count = settings.Count;
            _settings = settings;
        }

        internal override void AdvanceTime(float deltaTime)
        {
            while (_count > 0)
            {
                _count--;
                PlayerShipSettings playerShipSettings = new PlayerShipSettings(_viewportConnection, _settings);
                Entity newEntity = new PlayerShip(playerShipSettings);
                OnEntityCreated(newEntity);
            }
        }
    }
}