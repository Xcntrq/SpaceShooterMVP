namespace SpaceShooterGame.Contracts.Public
{
    using System;

    public interface IDestroyable
    {
        public event Action<bool> Destroyed;
    }
}