using TMPro;
using UnityEngine;

namespace Code
{
    public sealed class Finger : Weapon
    {
        [SerializeField] private FatalShot _fatalShotPrefab;
        [SerializeField] private TMP_Text _countText;
        private int _currentCountInClip;
        private int _countInClip = 1;
        private FatalShot _instantiateFatalShot;

        protected override void Start()
        {
            base.Start();

            Recharge();
        }

        public override void Fire()
        {
            if (_instantiateFatalShot)
            {
                _instantiateFatalShot.Run(_barrel.forward * Force);
                _instantiateFatalShot = null;
                _currentCountInClip--;
            }

            OnCompleteUpdateProgress();
        }

        public override void Recharge()
        {
            _currentCountInClip = _countInClip;
            OnCompleteUpdateProgress();

            if (_instantiateFatalShot != null)
            {
                return;
            }
            _instantiateFatalShot = Instantiate(_fatalShotPrefab, _barrel);
            _instantiateFatalShot.Sleep(_barrel.position);
        }
        private void OnCompleteUpdateProgress()
        {
            _currentCountInClip = Mathf.Clamp(_currentCountInClip, 0, _countInClip);
            _countText.text = $"{_currentCountInClip}/{_countInClip}";
        }
    }
}
