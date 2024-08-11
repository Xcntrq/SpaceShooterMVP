namespace PresenterMoBe
{
    using System;

    internal interface IViewportChangeDetector
    {
        public Action OnScreenHeightChanged { set; }
        public Action OnAspectRatioChanged { set; }
    }
}