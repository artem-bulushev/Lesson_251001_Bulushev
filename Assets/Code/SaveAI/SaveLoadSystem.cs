using UnityEngine;
using System.Collections.Generic;

namespace Code
{

    public class SaveLoadSystem : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private string targetTag = "Target";

        private SavedGameState savedState;
        private List<GameObject> allTargets = new List<GameObject>();

        private void Start()
        {
            // Находим все мишени на сцене при старте  
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            allTargets.AddRange(targets);

            // Инициализируем сохранённое состояние  
            savedState = new SavedGameState();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SaveGameState();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadGameState();
            }
        }

        private void SaveGameState()
        {
            // Сохраняем позицию и поворот персонажа  
            savedState.playerPosition = player.position;
            savedState.playerRotation = player.rotation;

            // Сохраняем состояние мишеней  
            savedState.activeTargets.Clear();
            foreach (var target in allTargets)
            {
                if (target != null && target.activeInHierarchy)
                {
                    savedState.activeTargets.Add(target.name);
                }
            }

            Debug.Log("Game state saved! Active targets: " + savedState.activeTargets.Count);
        }

        private void LoadGameState()
        {
            if (savedState == null) return;

            // Восстанавливаем позицию персонажа  
            player.position = savedState.playerPosition;
            player.rotation = savedState.playerRotation;

            // Восстанавливаем состояние мишеней  
            foreach (var target in allTargets)
            {
                if (target != null)
                {
                    // Активируем только те мишени, которые были активны при сохранении  
                    bool shouldBeActive = savedState.activeTargets.Contains(target.name);
                    target.SetActive(shouldBeActive);
                }
            }

            Debug.Log("Game state loaded! Active targets: " + savedState.activeTargets.Count);
        }

        // Класс для хранения состояния игры  
        [System.Serializable]
        private class SavedGameState
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
            public List<string> activeTargets = new List<string>();
        }
    }
}