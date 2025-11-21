using System;
using TMPro;
using UnityEngine;

namespace Code
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int _level = 1;
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected WeaponUpgradeData _weaponUpgradeData;

        protected float LastShootTime { get; set; }
        protected bool CanShoot { get; private set; }
        protected float Force { get; private set; }

        private float _shotDelay;

        protected virtual void Start()
        {
            if (_weaponUpgradeData!=null && _weaponUpgradeData.TryGetDataByLevel(_level, out WeaponData data))
            {
                _shotDelay = data.ShotDelay;
                Force = data.Force;
            }
            else
            {
                WeaponData defaultData = _weaponUpgradeData.GetDefaultData();
                _shotDelay = defaultData.ShotDelay;
                Force = defaultData.Force;
            }
        }

        protected virtual void Update()
        {
            CanShoot = _shotDelay <= LastShootTime;

            if (CanShoot)
            {
                return;
            }

            LastShootTime += Time.deltaTime;
        }

        public abstract void Fire();

        public abstract void Recharge();

        public virtual void GetInfo()
        {
            Debug.LogError(_shotDelay);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
