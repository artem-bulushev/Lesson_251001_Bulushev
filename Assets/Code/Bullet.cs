using System.Collections;
using UnityEngine;

namespace Code
{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 1.0f;
        [SerializeField] private float _lifeTime = 7.0f;

        private Rigidbody _rigidbody;
        private float _force = 3.0f;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _force = Random.Range(3.0f, 47.0f);
        }

        private void OnCollisionEnter(Collision other)
        {
            bool tryRicochet = TryRicochet();
            if (tryRicochet == false)
            {
                Destroy(gameObject);
            }

            if (other.collider.TryGetComponent(out HealthController health))
            {
                if (health.CanTakeDamage(_damage))
                {
                    if (tryRicochet == false)
                    {
                        ContactPoint contact = other.contacts[0];
                    }
                    return;
                }

                RigidbodyHelper
                    .AddOrGetComponent(other.collider.gameObject)
                    .AddForce(_rigidbody.velocity * _force, ForceMode.Impulse);
            }

            if (other.collider.TryGetComponent(out HealthEnemy healthEnemy))
            {
                if (healthEnemy.CanTakeDamageEnemy(_damage))
                {
                    if (tryRicochet == false)
                    {
                        ContactPoint contact = other.contacts[0];
                    }
                    return;
                }

                RigidbodyHelper
                    .AddOrGetComponent(other.collider.gameObject)
                    .AddForce(_rigidbody.velocity * _force, ForceMode.Impulse);
            }

        }

        private bool TryRicochet()
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                return false;
            }

            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 1.0f))
            {
                Vector3 reflect = Vector3.Reflect(ray.direction, hit.normal);
                transform.rotation = Quaternion.LookRotation(reflect);
                _force /= 2.0f;
                _rigidbody.AddForce(_rigidbody.velocity * _force, ForceMode.Impulse);
                return true;
            }

            return false;
        }

        public void Sleep()
        {
            _rigidbody.Sleep();
            gameObject.SetActive(false);
            IsActive = false;
        }

        public void Run(Vector3 path, Vector3 startPosition)
        {
            transform.position = startPosition;
            gameObject.SetActive(true);
            _rigidbody.WakeUp();
            _rigidbody.AddForce(path, ForceMode.Impulse);
            transform.SetParent(null);
            IsActive = true;
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            while (_lifeTime >= 0.0f)
            {
                _lifeTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }

            Destroy(gameObject);
        }
    }
}
