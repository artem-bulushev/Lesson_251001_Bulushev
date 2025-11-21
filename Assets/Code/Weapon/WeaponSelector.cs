using UnityEngine;
using System;

namespace Code
{
    public sealed class WeaponSelector
    {
        private int _currentIndex;
        private Weapon _currentWeapon;

        private readonly Weapon[] _weapons;

        public WeaponSelector(Weapon[] weapons)
        {
            _weapons = weapons;

            for (var index = 0; index < _weapons.Length; index++)
            {
                Weapon weapon = _weapons[index];
                weapon.SetActive(false);
            }
        }

        public void Fire()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Fire();
            }
        }

        public void Recharge()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Recharge();
            }
        }

        public void Next()
        {
            _currentIndex++;
            SelectWeapon();
        }

        public void Preview()
        {
            _currentIndex--;
            SelectWeapon();
        }

        private void SelectWeapon()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.SetActive(false);
            }

            int index = Mathf.Abs(_currentIndex % _weapons.Length);

            _currentWeapon = _weapons[index];
            _currentWeapon.SetActive(true);
        }

        public void Select(int index)
        {
            _currentIndex = index;
            SelectWeapon();
        }
    }
}
