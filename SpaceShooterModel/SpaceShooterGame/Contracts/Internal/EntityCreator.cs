namespace SpaceShooterGame.Contracts.Internal
{
    using System;

    internal abstract class EntityCreator : Entity
    {
        internal abstract event Action<Entity> EntityCreated;
    }
}