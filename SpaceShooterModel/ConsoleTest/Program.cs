namespace ConsoleTest
{
    using SpaceShooterGame;

    internal class Program
    {
        internal static void Main()
        {
            Game game = new();
            game.PresentableEntityCreated += Game_PresentableEntityCreated;
            game.Start();
        }

        private static void Game_PresentableEntityCreated(IPresentableEntity presentableEntity)
        {
        }
    }
}