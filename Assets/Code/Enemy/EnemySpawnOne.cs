using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class EnemySpawnOne : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _triggerRadius = 3f;
        [SerializeField] private float _spawnRadius = 2f;
        [SerializeField] private Transform _targetRoot;
        private bool _spawnOnlyOnce = true;

        private List<Transform> _spawnPoints = new List<Transform>();
        private Dictionary<Transform, bool> _spawnedPoints = new Dictionary<Transform, bool>();
        private Transform _playerTransform;

        //private Transform[] _spawnPoints;

        private void Awake()
        {
            InitializeSpawnPoints();
        }

        private void InitializeSpawnPoints()
        {
            _spawnPoints.Clear();
            _spawnedPoints.Clear();

            foreach (Transform child in transform)
            {
                if (child == null) continue;

                _spawnPoints.Add(child);
                _spawnedPoints[child] = false;

                var collider = child.GetComponent<SphereCollider>();
                if (collider == null)
                {
                    collider = child.gameObject.AddComponent<SphereCollider>();
                }
                collider.radius = _triggerRadius;
                collider.isTrigger = true;
            }
        }

        private void Update()
        {
            if (_playerTransform == null)
            {
                FindPlayer();
                return;
            }

            CheckSpawnPoints();
        }

        private void FindPlayer()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
            }
        }

        private void CheckSpawnPoints()
        {
            
            // Создаем копию списка для безопасной итерации
            var pointsToCheck = new List<Transform>(_spawnPoints);

            foreach (var point in pointsToCheck)
            {
                // Проверяем, не был ли объект уничтожен
                if (point == null || !point.gameObject.activeInHierarchy)
                {
                    _spawnPoints.Remove(point);
                    _spawnedPoints.Remove(point);
                    continue;
                }

                if (_spawnOnlyOnce && _spawnedPoints.TryGetValue(point, out var hasSpawned) && hasSpawned)
                    continue;

                //foreach (Transform child in _targetRoot)
                //{
                //    Destroy(child.gameObject);
                //}

                float distance = Vector3.Distance(_playerTransform.position, point.position);
                if (distance <= _triggerRadius)
                {
                    SpawnEnemy(point);
                    _spawnedPoints[point] = true;
                }
            }
        }

        private void SpawnEnemy(Transform spawnPoint)
        {
            if (spawnPoint == null) return;

            Vector3 randomOffset = Random.insideUnitSphere * _spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Враг заспавнен в точке {spawnPoint.name}");
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                InitializeSpawnPoints();
            }

            Gizmos.color = Color.yellow;
            foreach (Transform point in _spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, _triggerRadius);
                }
            }

            Gizmos.color = Color.red;
            foreach (Transform point in _spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, _spawnRadius);
                }
            }
        }

        //private void Start()
        //{
        //    _spawnPoints = GetComponentsInChildren<Transform>();
        //    Spawn();
        //}

        //private void Spawn()
        //{
        //    if (_spawnPoints.Length == 0) return;
        //    if (_enemyPrefab == null) return;

        //    foreach (var point in _spawnPoints)
        //    {
        //        if (point != transform) 
        //        {
        //            Instantiate(_enemyPrefab, point.position, Quaternion.identity);
        //        }    
        //    }
        //    //var enemy = Instantiate(_enemyPrefab, _spawnPoints[_spawnPoints.Length]);
        //}
    }
}
