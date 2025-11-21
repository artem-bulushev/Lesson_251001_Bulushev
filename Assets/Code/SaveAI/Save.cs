using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    public class Save : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private GameObject targetPrefab; // Резервный префаб

        private SavedGameState savedState;
        private List<TargetData> allTargets = new List<TargetData>();

        private void Start()
        {
            FindAllTargets();
            savedState = new SavedGameState();
        }

        private void FindAllTargets()
        {
            allTargets.Clear();
            GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in sceneObjects)
            {
                if (IsInTargetLayer(obj))
                {
                    allTargets.Add(new TargetData
                    {
                        position = obj.transform.position,
                        rotation = obj.transform.rotation,
                        scale = obj.transform.localScale,
                        instanceID = obj.GetInstanceID(),
                        gameObject = obj // Сохраняем ссылку на сам объект
                    });
                }
            }
        }

        private bool IsInTargetLayer(GameObject obj)
        {
            return targetLayer == (targetLayer | (1 << obj.layer));
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
            savedState.playerPosition = player.position;
            savedState.playerRotation = player.rotation;

            savedState.destroyedTargets.Clear();

            foreach (var target in allTargets)
            {
                if (target.gameObject == null)
                {
                    savedState.destroyedTargets.Add(target.instanceID);
                }
            }

            Debug.Log($"Game saved! Destroyed targets: {savedState.destroyedTargets.Count}");
        }

        private void LoadGameState()
        {
            if (savedState == null) return;

            // Восстанавливаем игрока
            player.position = savedState.playerPosition;
            player.rotation = savedState.playerRotation;

            // Восстанавливаем мишени
            foreach (var target in allTargets)
            {
                if (savedState.destroyedTargets.Contains(target.instanceID))
                {
                    // Если объект был уничтожен - создаем новый
                    if (target.gameObject == null)
                    {
                        GameObject newObj = Instantiate(
                            targetPrefab, // Используем резервный префаб
                            target.position,
                            target.rotation
                        );
                        newObj.transform.localScale = target.scale;
                        newObj.layer = LayerMask.NameToLayer(LayerMask.LayerToName(targetLayer));

                        // Обновляем ссылку
                        target.gameObject = newObj;
                    }
                }
                else if (target.gameObject != null)
                {
                    // Если объект существует - деактивируем (на случай если он активен)
                    target.gameObject.SetActive(true);
                }
            }

            Debug.Log($"Game loaded! Restored targets: {savedState.destroyedTargets.Count}");
        }

        [System.Serializable]
        private class SavedGameState
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
            public List<int> destroyedTargets = new List<int>();
        }

        [System.Serializable]
        private class TargetData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
            public int instanceID;
            [System.NonSerialized] public GameObject gameObject; // Не сериализуем, только для runtime
        }
    }
}