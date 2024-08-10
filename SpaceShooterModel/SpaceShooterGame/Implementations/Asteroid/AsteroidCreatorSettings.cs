namespace SpaceShooterGame.Implementations.Asteroid
{
    public struct AsteroidCreatorSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsteroidCreatorSettings"/> struct.
        /// </summary>
        /// <param name="count">Negative numbers mean infinity.</param>
        /// <param name="speed">a.</param>
        /// <param name="minSize">b.</param>
        /// <param name="maxSize">c.</param>
        /// <param name="cooldown">d.</param>
        public AsteroidCreatorSettings(int count = -1, float speed = 0.5f, float minSize = 0.05f, float maxSize = 0.15f, float cooldown = 0.5f)
        {
            Speed = speed;
            MinSize = minSize;
            MaxSize = maxSize;
            Cooldown = cooldown;
            Count = count;
        }

        internal float Speed { get; private set; }
        internal float MinSize { get; private set; }
        internal float MaxSize { get; private set; }
        internal float Cooldown { get; private set; }
        internal int Count { get; private set; }
    }
}