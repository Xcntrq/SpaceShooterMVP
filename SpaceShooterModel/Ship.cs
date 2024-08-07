namespace SpaceShooterModel
{
    using System.Numerics;

    internal class Ship : IPositionable
    {
        private readonly float speed;

        private float x;
        private float y;
        private float targetX;
        private float targetY;

        internal Ship(float x, float y, float speed)
        {
            this.speed = speed;
            this.x = x;
            this.y = y;
        }

        public float X => this.x;

        public float Y => this.y;

        internal void SetDestination(float x, float y)
        {
            this.targetX = x;
            this.targetY = y;
        }

        internal void Move(float deltaTime)
        {
            Vector2 p = new Vector2(this.x, this.y);
            Vector2 d = new Vector2(this.targetX, this.targetY);
            Vector2 o = Vector2.Normalize(d - p) * this.speed * deltaTime;
            this.x += o.X;
            this.y += o.Y;
        }
    }
}