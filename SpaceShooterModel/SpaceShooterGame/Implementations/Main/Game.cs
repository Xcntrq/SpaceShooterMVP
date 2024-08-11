namespace SpaceShooterGame.Implementations.Main
{
    using System;
    using System.Collections.Generic;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Asteroid;
    using SpaceShooterGame.Implementations.PlayerShip;

    public partial class Game
    {
        private readonly PlayerShipCreatorSettings _playerShipCreatorSettings;
        private readonly AsteroidCreatorSettings _asteroidCreatorSettings;
        private readonly List<Entity> _entities = new List<Entity>();
        private readonly ViewportConnection _viewportConnection;

        public Game(PlayerShipCreatorSettings? playerShipCreatorSettings = null, AsteroidCreatorSettings? asteroidCreatorSettings = null)
        {
            _playerShipCreatorSettings = playerShipCreatorSettings ?? new PlayerShipCreatorSettings(1);
            _asteroidCreatorSettings = asteroidCreatorSettings ?? new AsteroidCreatorSettings(-1);
            _viewportConnection = new ViewportConnection(1.8f);
        }

        public event Action<IPresentable>? PresentableEntityCreated;

        public void SetScreenHeight(int screenHeight)
        {
            _viewportConnection.SetScreenHeight(screenHeight);
        }

        public void SetAspectRatio(float aspectRatio)
        {
            _viewportConnection.SetAspectRatio(aspectRatio);
        }

        /// <summary>
        /// Consider calling SetAspectRatio() before Start().
        /// </summary>
        public void Start()
        {
            AddCreator(new PlayerShipCreator(_viewportConnection, _playerShipCreatorSettings));
            AddCreator(new AsteroidCreator(_viewportConnection, _asteroidCreatorSettings));
        }

        public void AdvanceTime(float deltaTime)
        {
            IEnumerable<Entity> entities = new List<Entity>(_entities);
            foreach (Entity entity in entities)
            {
                entity.AdvanceTime(deltaTime);
            }
        }

        private void AddCreator(EntityCreator entityCreator)
        {
            _entities.Add(entityCreator);
            entityCreator.EntityCreated += RegisterEntity;
            entityCreator.AdvanceTime(0);
        }

        private void RegisterEntity(Entity entity)
        {
            _entities.Add(entity);
            if (entity is IPresentable presentable)
            {
                PresentableEntityCreated?.Invoke(presentable);
            }
        }
    }
}