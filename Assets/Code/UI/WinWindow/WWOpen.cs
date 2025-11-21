using UnityEngine;

namespace Code
{
    public class WWOpen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvasWin;

        private void Start()
        {
            _canvasWin.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                _canvasWin.gameObject.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}