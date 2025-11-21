using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class Explosion : MonoBehaviour
    {
        private Light _light;
        
        private void Awake()
        {
            _light = gameObject.AddComponent<Light>();
            _light.type = LightType.Point;
            if (ColorUtility.TryParseHtmlString("#FFE700", out Color color))
            {
                _light.color = color;
            }
        }

        private IEnumerator Start()
        {
            _light.transform.SetParent(null);
            float step = 1.0f;
            while (_light.intensity < 10)
            {
                _light.intensity += step;
                _light.range += step;
                yield return new WaitForSeconds(0.05f);
            }
              
            while (_light.intensity > 0)
            {
                _light.intensity -= step * 2;
                _light.range -= step * 2;
                yield return new WaitForSeconds(0.05f);
            }
            
            Destroy(gameObject);
        }
    }
}
