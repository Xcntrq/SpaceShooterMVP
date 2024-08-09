namespace PresenterActivator
{
    using SpaceShooterGame;

    internal abstract class EntityPresenter
    {
        public EntityPresenter(GamePresenter gamePresenter, IPresentableEntity presentableEntity, ViewableEntity viewableEntity)
        {
            GamePresenter = gamePresenter;
            PresentableEntity = presentableEntity;
            ViewableEntity = viewableEntity;
        }

        private protected GamePresenter GamePresenter { get; private set; }
        private protected IPresentableEntity PresentableEntity { get; private set; }
        private protected ViewableEntity ViewableEntity { get; private set; }
    }
}