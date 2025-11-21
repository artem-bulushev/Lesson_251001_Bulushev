using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    public class SaveLo : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask targetLayer;

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
                    // Сохраняем полные данные о мишени
                    allTargets.Add(new TargetData
                    {
                        prefabName = obj.name.Replace("(Clone)", ""),
                        position = obj.transform.position,
                        rotation = obj.transform.rotation,
                        scale = obj.transform.localScale,
                        instanceID = obj.GetInstanceID()
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

            // Запоминаем ID уничтоженных мишеней
            foreach (var target in allTargets)
            {
                GameObject obj = GetGameObjectByID(target.instanceID);
                if (obj == null) // Если объект был уничтожен
                {
                    savedState.destroyedTargets.Add(target.instanceID);
                }
            }

            Debug.Log($"Game state saved! Destroyed targets: {savedState.destroyedTargets.Count}");
        }

        private void LoadGameState()
        {
            if (savedState == null) return;

            // Восстанавливаем позицию игрока
            player.position = savedState.playerPosition;
            player.rotation = savedState.playerRotation;

            // Восстанавливаем все мишени
            foreach (var target in allTargets)
            {
                GameObject obj = GetGameObjectByID(target.instanceID);

                // Если объект был уничтожен - воссоздаем его
                if (obj == null && savedState.destroyedTargets.Contains(target.instanceID))
                {
                    obj = Instantiate(
                        Resources.Load<GameObject>(target.prefabName),
                        target.position,
                        target.rotation
                    );
                    obj.transform.localScale = target.scale;
                    obj.layer = LayerMask.NameToLayer(LayerMask.LayerToName(targetLayer));
                }
            }

            Debug.Log($"Game state loaded! Restored targets: {allTargets.Count - savedState.destroyedTargets.Count}");
        }

        private GameObject GetGameObjectByID(int instanceID)
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetInstanceID() == instanceID)
                    return obj;
            }
            return null;
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
            public string prefabName;
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
            public int instanceID;
        }
    }
}