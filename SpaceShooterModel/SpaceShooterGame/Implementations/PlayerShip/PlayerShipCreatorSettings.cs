namespace SpaceShooterGame.Implementations.PlayerShip
{
    public struct PlayerShipCreatorSettings
    {
        public PlayerShipCreatorSettings(int count = 1, float x = 0.5f, float y = 0.1f, float size = 0.1f, float speed = 0.5f)
        {
            X = x;
            Y = y;
            Size = size;
            Speed = speed;
            Count = count;
        }

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Size { get; private set; }
        public float Speed { get; private set; }
        public int Count { get; private set; }
    }
}