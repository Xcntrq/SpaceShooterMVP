namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class ImageScroller : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Material _material;

        private void Awake()
        {
            Image image = GetComponent<Image>();
            image.material = _material = new Material(image.material);
        }

        private void Update()
        {
            _material.mainTextureOffset += _speed * Time.deltaTime * Vector2.up;
        }
    }
}