namespace PresenterMoBe
{
    using System;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    internal class VariableRotationPresenter : EntityPresenter, IVariableRotation
    {
        private IVariableRotation _variableRotation;

        public event Action RotationChanged;

        public float Rotation => _variableRotation.Rotation;

        internal override void Initialize(GamePresenter _, IPresentable presentableEntity)
        {
            _variableRotation = presentableEntity as IVariableRotation;

            VariableRotation_RotationChanged();
            _variableRotation.RotationChanged += VariableRotation_RotationChanged;
        }

        private void VariableRotation_RotationChanged()
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, Rotation);
            RotationChanged?.Invoke();
        }

        private void OnDestroy()
        {
            if (_variableRotation != null)
            {
                _variableRotation.RotationChanged -= VariableRotation_RotationChanged;
            }
        }
    }
}