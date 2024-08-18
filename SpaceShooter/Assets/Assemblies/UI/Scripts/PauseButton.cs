namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Canvas _gameplayCanvas;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
                _gameplayCanvas.gameObject.SetActive(!_gameplayCanvas.gameObject.activeSelf);
            });
        }
    }
}