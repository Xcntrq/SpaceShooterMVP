namespace SpaceShooterGame.Implementations.Asteroid
{
    public struct AsteroidCreatorSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsteroidCreatorSettings"/> struct.
        /// </summary>
        /// <param name="count">Negative numbers mean infinity.</param>
        /// <param name="cooldown">d.</param>
        /// <param name="speed">a.</param>
        /// <param name="minSize">b.</param>
        /// <param name="maxSize">c.</param>
        public AsteroidCreatorSettings(int count = -1, float cooldown = 0.5f, float speed = 0.5f, float minSize = 0.05f, float maxSize = 0.15f)
        {
            Count = count;
            Speed = speed;
            MinSize = minSize;
            MaxSize = maxSize;
            Cooldown = cooldown;
        }

        internal int Count { get; private set; }
        internal float Speed { get; private set; }
        internal float MinSize { get; private set; }
        internal float MaxSize { get; private set; }
        internal float Cooldown { get; private set; }
    }
}