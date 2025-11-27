using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class HealthEnemy : MonoBehaviour
    {
        [SerializeField] private float _healthEnemy = 3.0f;
        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _colorChangeTime = 1.0f;

        private bool _isAlive = true;
        private float _maxHp;

        public static event Action OnEnemyDied;

        public float MaxHp
        {
            get
            {
                return _maxHp;
            }
        }

        private void Start()
        {
            _maxHp = _healthEnemy;
        }

        public bool CanTakeDamageEnemy(float damage)
        {
            if (_isAlive == false)
            {
                return false;
            }

            _healthEnemy -= damage;

            if (_healthEnemy <= 0)
            {
                Animator animator = GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false;
                }
                StartCoroutine(Die());
                _isAlive = false;
                return false;
            }
            
            return true;
        }

        public void Death()
        {
            OnEnemyDied?.Invoke();
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
            Death();
        }
    }
}
