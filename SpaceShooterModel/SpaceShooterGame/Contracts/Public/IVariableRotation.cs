namespace SpaceShooterGame.Contracts.Public
{
    using System;

    public interface IVariableRotation
    {
        public event Action RotationChanged;

        public float Rotation { get; }
    }
}