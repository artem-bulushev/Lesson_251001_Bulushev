using UnityEngine;

namespace Code
{
    public class ExampleGameSaver : MonoBehaviour
    {
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private Transform _targetRoot;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private CharacterController _characterController;

        private GameState _gameState;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Сохранение");
                SaveGame();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Загрузка");
                LoadGame();
            }
        }

        private void GetSaveData()
        {
            Transform[] targets = _targetRoot.GetComponentsInChildren<Transform>();
            _gameState = new GameState(targets.Length);
            _gameState.TargetCount = targets.Length;
            for (int i = 0; i < targets.Length; i++)
            {
                _gameState.TargetPositions[i] = targets[i].position;
            }
        }

        public void SaveGame()
        {
            GetSaveData();
            _gameState.PlayerPosition = _playerTransform.position;
            string json = JsonUtility.ToJson(_gameState);
            PlayerPrefs.SetString("GameSave", json);
            PlayerPrefs.Save();
        }

        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("GameSave"))
            {
                string json = PlayerPrefs.GetString("GameSave");
                _gameState = JsonUtility.FromJson<GameState>(json);
                _characterController.Move((Vector3)_gameState.PlayerPosition - _playerTransform.position);
            }
            DeleteSave();
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey("GameSave");
            PlayerPrefs.Save();
            _gameState = null;
        }
    }
}