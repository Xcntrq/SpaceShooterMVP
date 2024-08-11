namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;
    using UnityEngine;

    internal class GamePresenter : MonoBehaviour
    {
        private Game _game;
        private Camera _camera;

        internal Camera Camera => (_camera != null) ? _camera : _camera = GetComponent<Camera>();

        internal Vector2 ScreenToViewportPoint(Vector2 point) => Camera.ScreenToViewportPoint(point);
        internal Vector2 ViewportToWorldPoint(Vector2 point) => Camera.ViewportToWorldPoint(point);
        internal bool IsPixelInViewport(Vector2 point) => new Rect(Vector2.zero, Vector2.one).Contains(Camera.ScreenToViewportPoint(point));

        private void Awake()
        {
            _game = new();
            _game.PresentableEntityCreated += Game_PresentableEntityCreated;
            _game.SetAspectRatio(Camera.aspect);
            _game.Start();

            IViewportChangeDetector viewportChangeDetector = GetComponentInChildren<IViewportChangeDetector>(true);
            viewportChangeDetector.OnScreenHeightChanged = () => _game.SetScreenHeight(Screen.height);
            viewportChangeDetector.OnAspectRatioChanged = () => _game.SetAspectRatio(Camera.aspect);
        }

        private void LateUpdate()
        {
            _game.AdvanceTime(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.PresentableEntityCreated -= Game_PresentableEntityCreated;
        }

        private void Game_PresentableEntityCreated(IPresentable presentableEntity)
        {
            GameObject go = new() { name = presentableEntity.Name };
            var modelTypes = typeof(IPresentable).Assembly.GetTypes();
            var presenterTypes = typeof(GamePresenter).Assembly.GetTypes();

            foreach (var modelType in modelTypes)
            {
                if (!modelType.IsPublic || !modelType.IsInstanceOfType(presentableEntity)) continue;

                foreach (var presenterType in presenterTypes)
                {
                    if (!modelType.IsAssignableFrom(presenterType) || !typeof(EntityPresenter).IsAssignableFrom(presenterType)) continue;

                    (go.AddComponent(presenterType) as EntityPresenter).Initialize(this, presentableEntity);
                }
            }
        }
    }
}