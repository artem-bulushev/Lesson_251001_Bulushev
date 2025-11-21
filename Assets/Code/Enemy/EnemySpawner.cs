using System.Collections;
using UnityEngine;

namespace Code
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemyLimit = 10;
        [SerializeField] private int _minSpawnDealy = 5;
        [SerializeField] private int _maxSpawnDealy = 10;

        private Transform[] _spawnPoints;

        private int _enemyCount = 1;

        private void OnEnable()
        {
            HealthEnemy.OnEnemyDied += KillEnemy;
        }

        private void Start()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
            StartCoroutine(SpawnEnemy());
        }

        private void Spawn()
        {
            var enemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(1, _spawnPoints.Length)]);
            _enemyCount++;
        }

        public void KillEnemy()
        {
            _enemyCount--;
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                if (_enemyCount < _enemyLimit)
                {
                    yield return new WaitForSeconds(Random.Range(_minSpawnDealy, _maxSpawnDealy + 1));
                    Spawn();
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}