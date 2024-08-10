namespace SpaceShooterGame
{
    public struct PlayerShipSettings
    {
        public PlayerShipSettings(float startPosX, float startPosY, float speed, float size)
        {
            StartPosX = startPosX;
            StartPosY = startPosY;
            Speed = speed;
            Size = size;
        }

        public float StartPosX { get; private set; }
        public float StartPosY { get; private set; }
        public float Speed { get; private set; }
        public float Size { get; private set; }
    }
}