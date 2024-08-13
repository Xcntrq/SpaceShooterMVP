namespace SpaceShooterGame.Contracts.Internal
{
    using System;

    internal abstract class Entity
    {
        internal abstract event Action Destroying;

        internal abstract void AdvanceTime(float deltaTime);
    }
}