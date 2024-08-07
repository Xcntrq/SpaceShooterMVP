namespace SpaceShooterModel
{
    using System;

    public class Game
    {
        private Ship? ship;

        public Game()
        {
        }

        public event Action? ShipCreated;

        public event Action<IPositionable?>? ShipMoved;

        public void Initialize()
        {
            this.ship = new Ship(0.5f, 0.1f, 1f);
            this.ShipCreated?.Invoke();
        }

        public void Update(float deltaTime)
        {
            this.ship?.Move(deltaTime);
            this.ShipMoved?.Invoke(this.ship);
        }

        public void SetShipDestination(float x, float y)
        {
            this.ship?.SetDestination(x, y);
        }
    }
}