namespace PresenterMoBe
{
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    internal class PhysicalPresenter : EntityPresenter, IPhysical
    {
        private IPhysical _physical;

        public IPhysical Physical => _physical;

        public void ProcessCollision(IPhysical anotherPhysical)
        {
            _physical.ProcessCollision(anotherPhysical);
        }

        internal override void Initialize(GamePresenter _, IPresentable presentableEntity)
        {
            _physical = presentableEntity as IPhysical;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
            CircleCollider2D col = GetComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.45f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PhysicalPresenter physicalPresenter))
            {
                ProcessCollision(physicalPresenter.Physical);
            }
        }
    }
}