namespace SpaceShooterGame.Contracts.Public
{
    using System;

    public interface IVariablePosition
    {
        public event Action PositionChanged;

        public (float, float) Position { get; }
    }
}