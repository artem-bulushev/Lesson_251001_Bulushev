using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class FatalShot : MonoBehaviour
    {
        private const int COLLISION_SIZE = 128;
        
        [SerializeField] private float _powerExplosion;
        [SerializeField] private float _scale;
        public bool IsActive { get; private set; }

        private Rigidbody _rigidbody;
        private readonly Collider[] _collidedObjects = new Collider[COLLISION_SIZE];
        private readonly ExplosionFactory _explosionFactory = new ();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            _explosionFactory.Create(transform.position);
            Destroy(gameObject);
            
            float radius = _scale * 0.5f;
            Vector3 center = other.contacts[0].point;
            int countCollied = Physics.OverlapSphereNonAlloc(center, radius, _collidedObjects);

            for (int i = 0; i < countCollied; i++)
            {
                Collider collidedObject = _collidedObjects[i];

                if (collidedObject.TryGetComponent(out HealthController healthController))
                {
                    if (healthController.CanTakeDamage(healthController.MaxHp))
                    {
                        return;
                    }

                    healthController.gameObject
                         .GetOrAddRigidbody()
                         .AddExplosionForce(_powerExplosion, center, radius);
                }

                if (collidedObject.TryGetComponent(out HealthEnemy healthEnemy))
                {
                    if (healthEnemy.CanTakeDamageEnemy(healthEnemy.MaxHp))
                    {
                        return;
                    }

                    healthEnemy.gameObject
                         .GetOrAddRigidbody()
                         .AddExplosionForce(_powerExplosion, center, radius);
                }
            }
        }
        public void Run(Vector3 path)
        {
            transform.SetParent(null);
            _rigidbody.WakeUp();
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(path, ForceMode.Impulse);
        }

        public void Sleep(Vector3 startPoint)
        {
            _rigidbody.Sleep();
            _rigidbody.isKinematic = true;
            transform.position = startPoint;
        }
    }
}