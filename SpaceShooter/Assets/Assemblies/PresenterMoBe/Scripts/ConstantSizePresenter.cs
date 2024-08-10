namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    internal class ConstantSizePresenter : EntityPresenter, IConstantSize
    {
        private IConstantSize _constantSize;

        public float Size => _constantSize.Size;

        internal override void Initialize(GamePresenter _, IPresentable presentableEntity)
        {
            _constantSize = presentableEntity as IConstantSize;
            transform.localScale = new(Size, Size, 1f);
        }
    }
}