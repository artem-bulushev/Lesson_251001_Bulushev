using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class EnemySpawnOne : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;

        private Transform[] _spawnPoints;

        private void Start()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
            Spawn();
        }

        private void Spawn()
        {
            if (_spawnPoints.Length == 0) return;
            if (_enemyPrefab == null) return;

            foreach (var point in _spawnPoints)
            {
                if (point != transform) 
                {
                    Instantiate(_enemyPrefab, point.position, Quaternion.identity);
                }    
            }
            //var enemy = Instantiate(_enemyPrefab, _spawnPoints[_spawnPoints.Length]);
        }
    }
}
