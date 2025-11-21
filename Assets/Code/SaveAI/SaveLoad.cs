using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    public class SaveLoad : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask targetLayer; // Слой мишеней

        private SavedGameState savedState;
        private List<GameObject> allTargets = new List<GameObject>();

        private void Start()
        {
            // Находим все объекты на слое мишеней
            FindAllTargets();

            savedState = new SavedGameState();
        }

        private void FindAllTargets()
        {
            allTargets.Clear();
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (IsInTargetLayer(obj))
                {
                    allTargets.Add(obj);
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

            savedState.activeTargets.Clear();
            foreach (var target in allTargets)
            {
                if (target != null && target.activeInHierarchy)
                {
                    savedState.activeTargets.Add(target.GetInstanceID());
                }
            }

            Debug.Log($"Game state saved! Active targets: {savedState.activeTargets.Count}");
        }

        private void LoadGameState()
        {
            if (savedState == null) return;

            player.position = savedState.playerPosition;
            player.rotation = savedState.playerRotation;

            foreach (var target in allTargets)
            {
                if (target != null)
                {
                    bool shouldBeActive = savedState.activeTargets.Contains(target.GetInstanceID());
                    target.SetActive(shouldBeActive);
                }
            }

            Debug.Log($"Game state loaded! Active targets: {savedState.activeTargets.Count}");
        }

        [System.Serializable]
        private class SavedGameState
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
            public List<int> activeTargets = new List<int>(); // Храним InstanceID вместо имён
        }
    }
}
