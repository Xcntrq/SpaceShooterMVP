namespace PresenterMoBe
{
    using System;
    using UnityEngine;

    internal class AspectChangeDetector : MonoBehaviour, IAspectChangeDetector
    {
        public event Action AspectRatioChanged;

        private void OnRectTransformDimensionsChange()
        {
            AspectRatioChanged?.Invoke();
        }
    }
}