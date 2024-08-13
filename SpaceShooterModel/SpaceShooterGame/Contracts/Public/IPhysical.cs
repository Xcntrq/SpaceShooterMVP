namespace SpaceShooterGame.Contracts.Public
{
    public interface IPhysical
    {
        public void ProcessCollision(IPhysical anotherPhysical);
    }
}