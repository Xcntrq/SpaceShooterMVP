namespace PresenterMoBe
{
    using System;
    using UnityEngine;

    internal class ViewportChangeDetector : MonoBehaviour, IViewportChangeDetector
    {
        public Action OnScreenHeightChanged { private get; set; }
        public Action OnAspectRatioChanged { private get; set; }

        private void OnRectTransformDimensionsChange()
        {
            OnScreenHeightChanged?.Invoke();
            OnAspectRatioChanged?.Invoke();
        }
    }
}