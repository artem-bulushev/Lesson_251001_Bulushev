using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = nameof(WeaponData), menuName = "Data/Data")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField] private float _force;
        [SerializeField] private float _shotDelay;
        
        public float Force
        {
            get
            {
                return _force;
            }
        }

        public float ShotDelay
        {
            get
            {
                return _shotDelay;
            }
        }
    }
}
