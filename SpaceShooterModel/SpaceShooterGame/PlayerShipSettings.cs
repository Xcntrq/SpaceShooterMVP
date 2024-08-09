namespace SpaceShooterGame
{
    public struct PlayerShipSettings
    {
        public PlayerShipSettings(float startPosX, float startPosY, float speed)
        {
            StartPosX = startPosX;
            StartPosY = startPosY;
            Speed = speed;
        }

        public float StartPosX { get; private set; }
        public float StartPosY { get; private set; }
        public float Speed { get; private set; }
    }
}