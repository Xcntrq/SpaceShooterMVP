namespace SpaceShooterGame.Contracts.Public
{
    using System;

    public interface IPlayerShip
    {
        public event Action<bool> Destroyed;

        public void SetDestination(float x, float y);
    }
}