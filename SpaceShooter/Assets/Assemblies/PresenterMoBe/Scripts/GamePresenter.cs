namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;
    using UnityEngine;

    internal class GamePresenter : MonoBehaviour
    {
        private Game _game;
        private Camera _camera;
        private IAspectChangeDetector _aspectChangeDetector;

        private Camera Camera => (_camera != null) ? _camera : _camera = GetComponent<Camera>();
        private IAspectChangeDetector AspectChangeDetector => _aspectChangeDetector ??= GetComponentInChildren<IAspectChangeDetector>(true);

        public Vector2 ScreenToViewportPoint(Vector2 point) => Camera.ScreenToViewportPoint(point);
        public Vector2 ViewportToWorldPoint(Vector2 point) => Camera.ViewportToWorldPoint(point);

        private void Awake()
        {
            _game = new();
            _game.PresentableEntityCreated += Game_PresentableEntityCreated;
            AspectChangeDetector.AspectRatioChanged += AspectChangeDetector_AspectRatioChanged;
            _game.SetAspectRatio(Camera.aspect);
            _game.Start();
        }

        private void AspectChangeDetector_AspectRatioChanged()
        {
            _game.SetAspectRatio(Camera.aspect);
        }

        private void LateUpdate()
        {
            _game.AdvanceTime(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.PresentableEntityCreated -= Game_PresentableEntityCreated;
            AspectChangeDetector.AspectRatioChanged -= AspectChangeDetector_AspectRatioChanged;
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