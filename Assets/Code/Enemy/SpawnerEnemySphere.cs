using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SpawnerEnemySphere : MonoBehaviour
    {
        [System.Serializable]
        public class PoolSettings
        {
            public GameObject prefab;
            public int initialPoolSize = 10;
            public Transform spawnPoint;
            public bool expandable = true;
        }

        [SerializeField] private PoolSettings _settings;
        private Queue<GameObject> _pool = new Queue<GameObject>();
        private List<GameObject> _activeObjects = new List<GameObject>();

        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            if (_settings.spawnPoint == null)
            {
                _settings.spawnPoint = transform;
                Debug.LogWarning("Spawn point not set, using pool transform as default");
            }

            for (int i = 0; i < _settings.initialPoolSize; i++)
            {
                CreatePooledObject();
            }
        }

        private GameObject CreatePooledObject()
        {
            if (_settings.prefab == null)
            {
                Debug.LogError("Prefab not assigned in pool settings!");
                return null;
            }

            var obj = Instantiate(_settings.prefab, _settings.spawnPoint.position, Quaternion.identity);
            obj.SetActive(true);
            obj.transform.SetParent(_settings.spawnPoint);
            _pool.Enqueue(obj);
            return obj;
        }

        public GameObject GetObject()
        {
            if (_pool.Count == 0)
            {
                if (_settings.expandable)
                {
                    Debug.LogWarning("Pool empty, creating new object");
                    return CreatePooledObject();
                }
                else
                {
                    Debug.LogWarning("Pool empty and not expandable!");
                    return null;
                }
            }

            var obj = _pool.Dequeue();
            _activeObjects.Add(obj);
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            if (!_activeObjects.Contains(obj))
            {
                Debug.LogWarning("Trying to return object not from this pool!");
                return;
            }

            obj.SetActive(false);
            obj.transform.position = _settings.spawnPoint.position;
            obj.transform.rotation = Quaternion.identity;
            _activeObjects.Remove(obj);
            _pool.Enqueue(obj);
        }

        public void ReturnAllObjects()
        {
            foreach (var obj in _activeObjects.ToArray())
            {
                ReturnObject(obj);
            }
        }

        // Для тестирования в редакторе
        [ContextMenu("Spawn Test Object")]
        private void SpawnTestObject()
        {
            var obj = GetObject();
            if (obj != null)
            {
                Debug.Log("Object spawned from pool");
            }
        }
    }
}
    
