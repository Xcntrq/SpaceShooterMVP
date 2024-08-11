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
        private readonly ScreenHeightProvider _screenHeightProvider;
        private readonly AspectRatioProvider _aspectRatioProvider;

        public Game(PlayerShipCreatorSettings? playerShipCreatorSettings = null, AsteroidCreatorSettings? asteroidCreatorSettings = null)
        {
            _playerShipCreatorSettings = playerShipCreatorSettings ?? new PlayerShipCreatorSettings(1);
            _asteroidCreatorSettings = asteroidCreatorSettings ?? new AsteroidCreatorSettings(-1);
            _screenHeightProvider = new ScreenHeightProvider();
            _aspectRatioProvider = new AspectRatioProvider(1.8f);
        }

        public event Action<IPresentable>? PresentableEntityCreated;

        public void SetScreenHeight(int screenHeight)
        {
            _screenHeightProvider.SetValue(screenHeight);
        }

        public void SetAspectRatio(float aspectRatio)
        {
            _aspectRatioProvider.SetValue(aspectRatio);
        }

        /// <summary>
        /// Consider calling SetAspectRatio() before Start().
        /// </summary>
        public void Start()
        {
            AddCreator(new PlayerShipCreator(_aspectRatioProvider, _screenHeightProvider, _playerShipCreatorSettings));
            AddCreator(new AsteroidCreator(_aspectRatioProvider, _asteroidCreatorSettings));
            AdvanceTime(0);
        }

        public void AdvanceTime(float deltaTime)
        {
            foreach (Entity entity in new List<Entity>(_entities))
            {
                entity.AdvanceTime(deltaTime);
            }
        }

        private void AddCreator(EntityCreator entityCreator)
        {
            _entities.Add(entityCreator);
            entityCreator.EntityCreated += RegisterEntity;
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