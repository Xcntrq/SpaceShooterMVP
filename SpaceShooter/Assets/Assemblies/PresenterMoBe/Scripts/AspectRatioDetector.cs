namespace PresenterMoBe
{
    using System;
    using UnityEngine;

    internal class AspectRatioDetector : MonoBehaviour, IAspectRatioDetector
    {
        public event Action AspectRatioChanged;

        private void OnRectTransformDimensionsChange()
        {
            AspectRatioChanged?.Invoke();
        }
    }
}