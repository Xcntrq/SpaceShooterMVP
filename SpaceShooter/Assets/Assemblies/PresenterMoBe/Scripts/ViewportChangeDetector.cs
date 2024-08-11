namespace PresenterMoBe
{
    using System;
    using UnityEngine;

    internal class ViewportChangeDetector : MonoBehaviour, IAspectRatioChangeDetector, IScreenHeightChangeDetector
    {
        public event Action AspectRatioChanged;
        public event Action ScreenHeightChanged;

        private void OnRectTransformDimensionsChange()
        {
            ScreenHeightChanged?.Invoke();
            AspectRatioChanged?.Invoke();
        }
    }
}