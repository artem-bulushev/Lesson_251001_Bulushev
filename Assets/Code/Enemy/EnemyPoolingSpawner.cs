using UnityEngine;
using UnityEngine.Pool;

namespace Code
{
    public class EnemyPoolingSpawner : MonoBehaviour
    {
        [SerializeField] private Shape _shapePrefab;
        [SerializeField] private int _spawnAmount = 20;
        [SerializeField] private bool _usePool;
        [SerializeField] private int _defaultCapacity = 100;
        [SerializeField] private int _maxCapacity = 800;
        private ObjectPool<Shape> _pool;

        private void Start()
        {
            _pool = new ObjectPool<Shape>(() =>
            {
                return
                Instantiate(_shapePrefab);
            },
            shape =>
            {
                shape.gameObject.SetActive(true);
            },
            shape =>
            {
                shape.gameObject.SetActive(false);
            },
            shape =>
            {
                Destroy(shape.gameObject);
            },
            false, _defaultCapacity, _maxCapacity);

            InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);
        }

        private void Spawn()
        {
            for (var i = 0; i < _spawnAmount; i++)
            {
                var shape = _usePool ? _pool.Get() : Instantiate(_shapePrefab);
                shape.transform.position = transform.position + Random.insideUnitSphere * 10;
                shape.Init(KillShape);
            }
        }

        private void KillShape(Shape shape)
        {
            if(_usePool)
            {
                _pool.Release(shape);
            }
            else
            {
                Destroy(shape.gameObject);
            }
        }
    }
}