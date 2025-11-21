using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class HealthController : MonoBehaviour
    {
        [SerializeField] private float _health = 3.0f;
        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _colorChangeTime = 1.0f;

        private bool _isAlive = true;
        private float _maxHp;

        public float MaxHp
        {
            get
            {
                return _maxHp;
            }
        }

        private void Start()
        {
            _maxHp = _health;
        }

        public bool CanTakeDamage(float damage)
        {
            if (_isAlive == false)
            {
                return false;
            }

            _health -= damage;

            if (_health <= 0)
            {
                StartCoroutine(Die());
                _isAlive = false;
                return false;
            }
            
            return true;
        }

        private IEnumerator Die()
        {
            var component = GetComponent<Renderer>();
            
            component.material.color = Color.red;
            yield return new WaitForSeconds(_colorChangeTime);
            component.material.color = Color.green;
            yield return new WaitForSeconds(_colorChangeTime);
            component.material.color = Color.red;
            yield return new WaitForSeconds(_colorChangeTime);
            component.material.color = Color.magenta;


            yield return new WaitForSeconds(_lifeTime);

            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                Color color = renderer.material.color;
                for (float alpha = 1.0f; alpha >= 0.0f; alpha -= 0.1f)
                {
                    color.a = alpha;
                    renderer.material.color = color;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            Destroy(gameObject);
        }
    }
}
