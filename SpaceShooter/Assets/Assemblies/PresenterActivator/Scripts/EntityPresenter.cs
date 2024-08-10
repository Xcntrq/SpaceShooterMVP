namespace PresenterActivator
{
    using SpaceShooterGame.Contracts.Public;

    internal abstract class EntityPresenter
    {
        public EntityPresenter(GamePresenter gamePresenter, IPresentable presentableEntity, ViewableEntity viewableEntity)
        {
            GamePresenter = gamePresenter;
            PresentableEntity = presentableEntity;
            ViewableEntity = viewableEntity;
        }

        private protected GamePresenter GamePresenter { get; private set; }
        private protected IPresentable PresentableEntity { get; private set; }
        private protected ViewableEntity ViewableEntity { get; private set; }
    }
}