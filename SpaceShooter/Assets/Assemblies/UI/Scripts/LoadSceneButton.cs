namespace UI
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [Header("Empty is GetActive")]
        [SerializeField] private string _name;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(string.IsNullOrEmpty(_name) ? SceneManager.GetActiveScene().name : _name);
            });
        }
    }
}