namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    internal abstract class EntityPresenter : MonoBehaviour
    {
        internal abstract void Initialize(GamePresenter gamePresenter, IPresentable presentableEntity);
    }
}