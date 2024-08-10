namespace PresenterMoBe
{
    using System;
    using SpaceShooterGame;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    internal class TrackableSizePresenter : EntityPresenter, ITrackableSize
    {
        private ITrackableSize _trackableSize;

        public event Action SizeChanged;

        public float Size => _trackableSize.Size;

        internal override void Initialize(GamePresenter _, IPresentableEntity presentableEntity)
        {
            _trackableSize = presentableEntity as ITrackableSize;

            TrackableSize_SizeChanged();
            _trackableSize.SizeChanged += TrackableSize_SizeChanged;
        }

        private void TrackableSize_SizeChanged()
        {
            transform.localScale = new(Size, Size, 1f);
            SizeChanged?.Invoke();
        }

        private void OnDestroy()
        {
            if (_trackableSize != null)
            {
                _trackableSize.SizeChanged -= TrackableSize_SizeChanged;
            }
        }
    }
}