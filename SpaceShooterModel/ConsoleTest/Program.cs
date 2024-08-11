namespace ConsoleTest
{
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;

    internal class Program
    {
        internal static void Main()
        {
            Game game = new();
            game.PresentableEntityCreated += Game_PresentableEntityCreated;
            game.Start();
        }

        private static void Game_PresentableEntityCreated(IPresentable presentableEntity)
        {
            Console.Write(presentableEntity.Name);
            if (presentableEntity is IPlayerShip playerShip)
            {
                if (playerShip is IVariablePosition position)
                {
                    Console.WriteLine(position.Position.ToString());
                    position.PositionChanged += () => Console.WriteLine(position.Position.ToString());
                }
            }
        }
    }
}