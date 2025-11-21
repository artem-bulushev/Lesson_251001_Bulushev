using UnityEngine;

namespace Code
{
    public class CanvasAuto: MonoBehaviour
    {
        [SerializeField] private Canvas _canvasToToggle;
        private Canvas _targetCanvas;

        private void Start()
        {
            _canvasToToggle.gameObject.SetActive(true);
        }

        private void Awake()
        {
            if (_canvasToToggle != null)
            {
                _targetCanvas = _canvasToToggle;
            }
            else
            {
                _targetCanvas = GetComponent<Canvas>();
                if (_targetCanvas == null)
                {
                    _targetCanvas = GetComponentInChildren<Canvas>(true);
                }
            }
        }

        private void OnEnable()
        {
            if (_targetCanvas != null)
            {
                _targetCanvas.enabled = true;
            }
        }

        private void OnDisable()
        {
            if (_targetCanvas != null)
            {
                _targetCanvas.enabled = false;
            }
        }
    }
}