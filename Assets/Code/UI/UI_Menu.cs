using UnityEngine;

namespace Code
{
    public class UI_Menu : MonoBehaviour
    {
        [SerializeField] private Canvas _canvasSetting;
        [SerializeField] private bool _pauseOnOpen = true;

        private void Start()
        {
            _canvasSetting.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VisibilityCanvas();
            }
        }

        private void VisibilityCanvas()
        {
            if (_canvasSetting == null) return;

            bool isOpen = _canvasSetting.gameObject.activeSelf;
            _canvasSetting.gameObject.SetActive(!isOpen);

            if (_pauseOnOpen)
            {
                Time.timeScale = isOpen ? 1f : 0f;
            }
        }
    }
}
