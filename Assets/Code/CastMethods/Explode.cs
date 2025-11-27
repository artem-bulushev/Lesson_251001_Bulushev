using UnityEngine;

namespace Code
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _force = 100f;
        [SerializeField] private float _modifier = 2f;
        [SerializeField] private ParticleSystem _particleSystem;

        public void ExplodeObj()
        {
            _particleSystem.Play();
            var colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_force, transform.position, _radius, _modifier, ForceMode.Impulse);
                }
            }

            Destroy(gameObject, 3);
        }
    }
}