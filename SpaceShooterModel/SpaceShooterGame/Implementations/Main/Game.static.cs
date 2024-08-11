namespace SpaceShooterGame.Implementations.Main
{
    using System.Numerics;
    using SpaceShooterGame.Contracts.Internal;

    public partial class Game
    {
        internal static (float, float) GameToViewport(Vector2 vector, IAspectRatioProvider aspectRatioProvider)
        {
            float x = vector.X / aspectRatioProvider.AspectRatio + 0.5f;
            float y = vector.Y + 0.5f;
            return (x, y);
        }

        internal static Vector2 ViewportToGame(float x, float y, IAspectRatioProvider aspectRatioProvider)
        {
            x = (x - 0.5f) * aspectRatioProvider.AspectRatio;
            return new Vector2(x, y - 0.5f);
        }
    }
}