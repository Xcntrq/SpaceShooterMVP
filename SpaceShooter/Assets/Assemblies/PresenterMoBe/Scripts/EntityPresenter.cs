namespace PresenterMoBe
{
    using SpaceShooterGame;
    using UnityEngine;

    internal abstract class EntityPresenter : MonoBehaviour
    {
        internal abstract void Initialize(GamePresenter gamePresenter, IPresentableEntity presentableEntity);
    }
}