using System;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = nameof(WeaponUpgradeData), menuName = "Data/Upgrade")]
    public sealed class WeaponUpgradeData : ScriptableObject
    {
        [Serializable]
        private class WeaponDataByLevel
        {
            public int Level;
            public WeaponData Data;
        }
        
        [SerializeField] private WeaponDataByLevel[] _weaponDataByLevels;
        [SerializeField] private WeaponData _weaponDataDefault;
        
        public WeaponData GetDefaultData()
        {
            return _weaponDataDefault;
        }

        public bool TryGetDataByLevel(int level, out WeaponData weaponData)
        {
            for (var index = 0; index < _weaponDataByLevels.Length; index++)
            {
                WeaponDataByLevel weaponDataByLevel = _weaponDataByLevels[index];
                if (weaponDataByLevel.Level == level)
                {
                    weaponData = weaponDataByLevel.Data;
                    return true;
                }
            }

            weaponData = default;
            return false;
        }
    }
}
