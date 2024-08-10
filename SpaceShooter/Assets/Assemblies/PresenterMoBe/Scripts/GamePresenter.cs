namespace PresenterMoBe
{
    using SpaceShooterGame;
    using UnityEngine;

    internal class GamePresenter : MonoBehaviour
    {
        private Game _game;
        private Camera _camera;
        private IAspectRatioDetector _aspectRatioDetector;

        private Camera Camera => (_camera != null) ? _camera : _camera = GetComponent<Camera>();
        private IAspectRatioDetector AspectRatioDetector => _aspectRatioDetector ??= GetComponentInChildren<IAspectRatioDetector>(true);

        public Vector2 ScreenToViewportPoint(Vector2 point) => Camera.ScreenToViewportPoint(point);
        public Vector2 ViewportToWorldPoint(Vector2 point) => Camera.ViewportToWorldPoint(point);

        private void Awake()
        {
            PlayerShipSettings playerShipSettings = new(0.5f, 0.1f, 0.5f, 0.1f);
            _game = new(Camera.aspect, playerShipSettings);
            _game.PresentableEntityCreated += Game_PresentableEntityCreated;
            AspectRatioDetector.AspectRatioChanged += AspectRatioDetector_AspectRatioChanged;
        }

        private void AspectRatioDetector_AspectRatioChanged()
        {
            _game.SetAspectRatio(Camera.aspect);
        }

        private void Start()
        {
            _game.Start();
        }

        private void LateUpdate()
        {
            _game.AdvanceTime(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.PresentableEntityCreated -= Game_PresentableEntityCreated;
            AspectRatioDetector.AspectRatioChanged -= AspectRatioDetector_AspectRatioChanged;
        }

        private void Game_PresentableEntityCreated(IPresentableEntity presentableEntity)
        {
            GameObject go = new() { name = presentableEntity.Name };
            var modelTypes = typeof(IPresentableEntity).Assembly.GetTypes();
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