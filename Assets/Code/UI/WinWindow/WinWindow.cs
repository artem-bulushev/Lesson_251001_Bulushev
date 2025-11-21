using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private CanvasGroup _windowCanvasGroup;
        [SerializeField] private CanvasGroup _contentCanvasGroup;
        [SerializeField] private float _showAnimationDuration = 1.0f;
        [SerializeField] private GameObject _coins;
        [SerializeField] private Transform _coinTarget;
        [SerializeField] private float _delay = 2f;

        private void Awake()
        {
            _giveUpButton.onClick.AddListener(WinHide);
            _rewardButton.onClick.AddListener(OnRawardLock);
            gameObject.SetActive(false);
            _coins.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            StartCoroutine(WinShow());
        }

        private void OnRawardLock()
        {
            Debug.Log("Reward log");
        }

        private IEnumerator WinShow()
        {
            gameObject.SetActive(true);
            _windowCanvasGroup.DOFade(1,_showAnimationDuration);
            yield return new WaitForSeconds(_showAnimationDuration);
            Debug.Log(_coinTarget.position);
            _coins.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _coins.SetActive(false);
            yield return new WaitForSeconds(_showAnimationDuration);
            _contentCanvasGroup.DOFade(1, _showAnimationDuration);
        }

        private void WinHide()
        {
            StartCoroutine(WinHideCO());
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private IEnumerator WinHideCO()
        {
            _contentCanvasGroup.DOFade(0, _showAnimationDuration);
            yield return new WaitForSeconds(_showAnimationDuration);
            _windowCanvasGroup.DOFade(0, _showAnimationDuration);
            gameObject.SetActive(false);
        }
    }
}