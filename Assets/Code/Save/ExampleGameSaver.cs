using StarterAssets;
using UnityEngine;

namespace Code
{
    public class ExampleGameSaver : MonoBehaviour
    {
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private Transform _targetRoot;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private CharacterController _characterController;
        //[SerializeField] private FirstPersonController _playerController;

        private GameState _gameState;

        //private void Awake()
        //{
        //    LoadGame();
        //}

        private void Start()
        {

            //if (_gameState != null)
            //{

            //    for (int i = 0; i < _gameState.TargetCount; i++)
            //    {
            //        var target = Instantiate(_targetPrefab, (Vector3)_gameState.TargetPositions[i], Quaternion.identity, _targetRoot);
            //    }
            //}
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Сохрани");
                SaveGame();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Загрузи");
                LoadGame();
            }
        }

        private void GetSaveData()
        {
            Transform[] targets = _targetRoot.GetComponentsInChildren<Transform>();
            _gameState = new GameState(targets.Length);
            //_gameState.PlayerPosition = _playerController.transform.position;
            //_gameState.PlayerPosition = _playerTransform.transform.position;
            _gameState.TargetCount = targets.Length;
            for (int i = 0; i < targets.Length; i++)
            {
                _gameState.TargetPositions[i] = targets[i].position;
            }
        }

        public void SaveGame()
        {
            //_gameState.PlayerRotation = _playerTransform.rotation;
            GetSaveData();
            _gameState.PlayerPosition = _playerTransform.position;
            string json = JsonUtility.ToJson(_gameState);
            Debug.Log(json);
            PlayerPrefs.SetString("GameSave", json);
            PlayerPrefs.Save();
        }

        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("GameSave"))
            {
                string json = PlayerPrefs.GetString("GameSave");
                _gameState = JsonUtility.FromJson<GameState>(json);
                Debug.Log(json);
                _characterController.Move((Vector3)_gameState.PlayerPosition - _playerTransform.position);
                //_playerTransform.Translate((Vector3)_gameState.PlayerPosition);
                //_playerTransform.transform.position = (Vector3)_gameState.PlayerPosition;
                Debug.Log("Уже лучше");
            }
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey("GameSave");
            PlayerPrefs.Save();
            _gameState = null;
        }
    }
}