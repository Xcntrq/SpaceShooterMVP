namespace SpaceShooterGame.Contracts.Public
{
    using System;

    public interface IAsteroid
    {
        public event Action<bool> Destroyed;
    }
}