using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class HP : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Canvas _canvasHp;
        public Scrollbar _healthScrollbar;
        private float _maxHp = 100f;
        private float _minHp = 0f;
        public float _currentHp;

        public void Start()
        {
            _currentHp = _maxHp;
            _canvasHp.gameObject.SetActive(true);
        }

        public void UpdateHealthBar()
        { 
            _healthScrollbar.size = _currentHp / _maxHp;
            _currentHp = Mathf.Clamp(_currentHp, _minHp, _maxHp);
        }

        private void OnCompleteUpdateProgress()
        {
            _countText.text = $"{(int)_currentHp}/{_maxHp}";
        }

        private void OnValidate()
        {
            EditorApplication.delayCall += () =>
            {
                if (_healthScrollbar != null)
                {
                    UpdateHealthBar();
                    OnCompleteUpdateProgress();
                }
            };
        }
    }
}