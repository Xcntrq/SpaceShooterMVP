namespace SpaceShooterGame.Contracts.Internal
{
    using System;

    internal abstract class EntityCreator : Entity
    {
        internal event Action<Entity>? EntityCreated;

        private protected void OnEntityCreated(Entity entity)
        {
            EntityCreated?.Invoke(entity);
        }
    }
}