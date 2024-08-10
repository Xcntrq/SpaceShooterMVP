namespace PresenterMoBe
{
    using System;

    internal interface IAspectChangeDetector
    {
        public event Action AspectRatioChanged;
    }
}