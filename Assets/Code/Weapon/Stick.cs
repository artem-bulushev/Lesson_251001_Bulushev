using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code
{
    public sealed class Stick : Weapon
    {
        [SerializeField] private int _countInClip = 1000;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private TMP_Text _countText;
        private int _currentCountInClip;

        private Transform _bulletRoot;
        private Queue<Bullet> _bullets;

        protected override void Start()
        {
            base.Start();

            _currentCountInClip = _countInClip;
            OnCompleteUpdateProgress();
            _bullets = new Queue<Bullet>(_countInClip);
            _bulletRoot = new GameObject("BulletRoot").transform;
            Recharge();
        }

        public override void Recharge()
        {
            if (_bullets.Count > 0)
            {
                DestroyBullet();
            }

            for (int i = 0; i < _countInClip; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets.Enqueue(bullet);
            }
            _currentCountInClip = _countInClip;
            OnCompleteUpdateProgress();
        }

        private void DestroyBullet()
        {
            foreach (Bullet bullet in _bullets)
            {
                Destroy(bullet.gameObject);
            }
            _bullets.Clear();
        }

        public override void Fire()
        {
            if (CanShoot == false)
            {
                return;
            }
            
            if (_bullets.TryDequeue(out Bullet bullet))
            {
                bullet.Run(_barrel.forward * Force, _barrel.position);
                LastShootTime = 0.0f;
                _currentCountInClip --;
            }

            OnCompleteUpdateProgress();
        }

        public override void GetInfo()
        {
            base.GetInfo();
            
            Debug.LogError(_countInClip);
        }

        private void OnCompleteUpdateProgress()
        {
            _currentCountInClip = Mathf.Clamp(_currentCountInClip, 0, _countInClip);
            _countText.text = $"{_currentCountInClip}/{_countInClip}";
        }
    }
}