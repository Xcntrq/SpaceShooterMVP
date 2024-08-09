namespace SpaceShooterGame
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private readonly List<Entity> _entities;

        public Game()
        {
            PlayerShipSettings playerShipSettings = new PlayerShipSettings(0.5f, 0.1f, 0.5f);
            _entities = new List<Entity>
            {
                new PlayerShip(playerShipSettings),
            };
        }

        public Game(PlayerShipSettings playerShipSettings)
        {
            _entities = new List<Entity>
            {
                new PlayerShip(playerShipSettings),
            };
        }

        public event Action<IPresentableEntity>? PresentableEntityCreated;

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