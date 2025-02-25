namespace PresenterActivator
{
    using System;
    using UnityEngine;

    internal class ViewableEntity : MonoBehaviour
    {
        internal event Action Updating;
        internal event Action Destroying;

        internal Vector2 Position { set => transform.localPosition = value; }

        internal T AddComponent<T>() where T : Component => gameObject.AddComponent<T>();

        private void Update() => Updating?.Invoke();

        private void OnDestroy() => Destroying?.Invoke();
    }
}