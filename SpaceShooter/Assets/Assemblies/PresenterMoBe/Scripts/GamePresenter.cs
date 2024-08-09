namespace PresenterMoBe
{
    using SpaceShooterGame;
    using UnityEngine;

    internal class GamePresenter : MonoBehaviour
    {
        private Game _game;

        public Vector2 ScreenToViewportPoint(Vector2 point) => GetComponent<Camera>().ScreenToViewportPoint(point);
        public Vector2 ViewportToWorldPoint(Vector2 point) => GetComponent<Camera>().ViewportToWorldPoint(point);

        private void Awake()
        {
            _game = new();
            _game.PresentableEntityCreated += Game_PresentableEntityCreated;
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