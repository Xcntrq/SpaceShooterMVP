namespace PresenterMoBe
{
    using System;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(GameObject))]
    internal class DestroyablePresenter : EntityPresenter, IDestroyable
    {
        private IDestroyable _destroyable;

        public event Action<bool> Destroyed;

        internal override void Initialize(GamePresenter _, IPresentable presentableEntity)
        {
            _destroyable = presentableEntity as IDestroyable;
            _destroyable.Destroyed += Destroyable_Destroyed;
        }

        private void Destroyable_Destroyed(bool hasEffect)
        {
            if (_destroyable != null)
            {
                _destroyable.Destroyed -= Destroyable_Destroyed;
            }

            Destroy(gameObject);
            Destroyed?.Invoke(false);
        }
    }
}