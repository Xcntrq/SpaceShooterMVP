namespace PresenterMoBe
{
    using System.Collections;
    using SpaceShooterGame.Contracts.Public;
    using SpaceShooterGame.Implementations.Main;
    using UnityEngine;

    internal class GamePresenter : MonoBehaviour
    {
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Canvas _lossCanvas;

        private Game _game;
        private Camera _camera;

        internal Camera Camera => (_camera != null) ? _camera : _camera = GetComponent<Camera>();
        internal Canvas GameplayCanvas => _gameplayCanvas;

        internal Vector2 ScreenToViewportPoint(Vector2 point) => Camera.ScreenToViewportPoint(point);
        internal Vector3 ViewportToWorldPoint(Vector2 point) => Camera.ViewportToWorldPoint(point);
        internal bool IsPixelInViewport(Vector2 point) => new Rect(Vector2.zero, Vector2.one).Contains(Camera.ScreenToViewportPoint(point));

        private void Awake()
        {
            // _game = new(null, new(-1, 2f));
            _game = new();
            _game.Lost += Game_Lost;
            _game.PhysicsUpdateRequested += Game_PhysicsUpdateRequested;
            _game.PresentableEntityCreated += Game_PresentableEntityCreated;
            _game.SetAspectRatio(Camera.aspect);
            _game.Start();

            Camera.allowHDR = true;
            IViewportChangeDetector viewportChangeDetector = GetComponentInChildren<IViewportChangeDetector>(true);
            viewportChangeDetector.OnScreenHeightChanged = () => _game.SetScreenHeight(Screen.height);
            viewportChangeDetector.OnAspectRatioChanged = () => _game.SetAspectRatio(Camera.aspect);
            Physics2D.simulationMode = SimulationMode2D.Script;
        }

        private void LateUpdate()
        {
            _game.AdvanceTime(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.PresentableEntityCreated -= Game_PresentableEntityCreated;
            _game.PhysicsUpdateRequested -= Game_PhysicsUpdateRequested;
            _game.Lost -= Game_Lost;
        }

        private void Game_Lost()
        {
            _gameplayCanvas.gameObject.SetActive(false);
            _pauseCanvas.gameObject.SetActive(false);
            StartCoroutine(ProcLoss());
        }

        private IEnumerator ProcLoss()
        {
            yield return new WaitForSeconds(2f);
            _lossCanvas.gameObject.SetActive(true);
        }

        private void Game_PhysicsUpdateRequested()
        {
            Physics2D.Simulate(0f);
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