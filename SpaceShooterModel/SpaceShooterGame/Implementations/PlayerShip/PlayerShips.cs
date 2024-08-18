namespace SpaceShooterGame.Implementations.PlayerShip
{
    using System.Collections.Generic;
    using SpaceShooterGame.Contracts.Internal;
    using SpaceShooterGame.Contracts.Public;

    internal class PlayerShips : List<IPlayerShip>
    {
        internal void TryAdd(Entity entity)
        {
            if (entity is IPlayerShip playerShip)
            {
                Add(playerShip);
            }
        }

        internal int TryRemoveAndCount(Entity entity)
        {
            if (entity is IPlayerShip playerShip)
            {
                Remove(playerShip);
            }

            return Count;
        }
    }
}