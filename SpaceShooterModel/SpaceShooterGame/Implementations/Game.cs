namespace SpaceShooterGame
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private readonly AspectRatio _aspectRatio;
        private readonly List<Entity> _entities;

        public Game()
        {
            PlayerShipSettings playerShipSettings = new PlayerShipSettings(0.5f, 0.1f, 0.5f, 0.1f);
            _aspectRatio = new AspectRatio(1.6f);
            _entities = new List<Entity>
            {
                new PlayerShip(playerShipSettings, AspectRatio),
            };
        }

        public Game(float aspectRatio, PlayerShipSettings playerShipSettings)
        {
            _aspectRatio = new AspectRatio(aspectRatio);
            _entities = new List<Entity>
            {
                new PlayerShip(playerShipSettings, AspectRatio),
            };
        }

        public event Action<IPresentableEntity>? PresentableEntityCreated;

        private IAspectRatio AspectRatio => _aspectRatio;

        public void SetAspectRatio(float aspectRatio)
        {
            _aspectRatio.SetValue(aspectRatio);
        }

        public void Start()
        {
            foreach (Entity entity in _entities)
            {
                if (entity is IPresentableEntity presentableEntity)
                {
                    PresentableEntityCreated?.Invoke(presentableEntity);
                }
            }
        }

        public void AdvanceTime(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                entity.AdvanceTime(deltaTime);
            }
        }
    }
}