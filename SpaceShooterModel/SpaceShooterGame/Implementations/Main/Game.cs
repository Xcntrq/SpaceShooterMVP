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
        public event Action? PhysicsUpdateRequested;

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
            while (deltaTime > 0)
            {
                float step = MathF.Min(deltaTime, 0.010f);
                deltaTime -= step;

                PhysicsUpdateRequested?.Invoke();

                IEnumerable<Entity> entities = new List<Entity>(_entities);
                foreach (Entity entity in entities)
                {
                    entity.AdvanceTime(step);
                }
            }
        }

        private void AddCreator(EntityCreator entityCreator)
        {
            entityCreator.EntityCreated += AddEntity;
            AddEntity(entityCreator);
        }

        private void AddEntity(Entity entity)
        {
            _entities.Add(entity);
            entity.Destroying += () => RemoveEntity(entity);
            if (entity is IPresentable presentable)
            {
                PresentableEntityCreated?.Invoke(presentable);
            }

            entity.AdvanceTime(0);
        }

        private void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }
    }
}