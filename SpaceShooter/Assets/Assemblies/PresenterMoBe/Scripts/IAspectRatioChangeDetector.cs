namespace PresenterMoBe
{
    using System;

    internal interface IAspectRatioChangeDetector
    {
        public event Action AspectRatioChanged;
    }
}