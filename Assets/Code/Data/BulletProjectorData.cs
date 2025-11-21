using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = nameof(BulletProjectorData), menuName = "Data/BulletProjectorData")]
    public sealed class BulletProjectorData : ScriptableObject
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _lightRange = 0.4f; 
        [SerializeField] private float _lightIntensity = 0.02f; 
        [SerializeField] private float _lightInnerSpotAngle = 7.0f;
        [SerializeField] private float _lightSpotAngle = 30.0f;

        public Material Material
        {
            get
            {
                return _material;
            }
        }

        public float LightRange
        {
            get
            {
                return _lightRange;
            }
        }

        public float LightIntensity
        {
            get
            {
                return _lightIntensity;
            }
        }

        public float LightInnerSpotAngle
        {
            get
            {
                return _lightInnerSpotAngle;
            }
        }

        public float LightSpotAngle
        {
            get
            {
                return _lightSpotAngle;
            }
        }
    }
}
