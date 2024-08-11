namespace PresenterMoBe
{
    using System;

    internal interface IScreenHeightChangeDetector
    {
        public event Action ScreenHeightChanged;
    }
}