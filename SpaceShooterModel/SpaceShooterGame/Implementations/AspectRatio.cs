namespace SpaceShooterGame
{
    using System;

    internal class AspectRatio : IAspectRatio
    {
        private float _value;

        internal AspectRatio(float value)
        {
            _value = value;
        }

        public event Action? ValueChanged;

        public float Value => _value;

        internal void SetValue(float newValue)
        {
            _value = newValue;
            ValueChanged?.Invoke();
        }
    }
}