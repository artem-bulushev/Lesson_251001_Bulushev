using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Code
{
    public class AttackBah : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume _postProcessVolume;
        [SerializeField] private float _explosionIntrsity;
        [SerializeField] private float _explosionDuration;
        [SerializeField] private float _fadeDuration;
        private Vignette _vignette;

        private void Awake()
        {
            if(_postProcessVolume.profile.TryGetSettings(out _vignette))
            {
                _vignette.enabled.value = false;
                _vignette.intensity.value = 0f;
            }
        }
        public void StartExplosionEffect()
        {
            StartCoroutine(ExplosionVignette());
        }

        private IEnumerator ExplosionVignette()
        {
            _vignette.enabled.value = true;
            float elapsedTime = 0f;

            while(elapsedTime < _explosionDuration)
            {
                elapsedTime += Time.deltaTime;
                _vignette.intensity.value = Mathf.Lerp(0f, _explosionIntrsity, elapsedTime/_explosionDuration);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(_fadeDuration);

            elapsedTime = 0f;

            while (elapsedTime < _explosionDuration)
            {
                elapsedTime += Time.deltaTime;
                _vignette.intensity.value = Mathf.Lerp(_explosionIntrsity, 0f, elapsedTime / _explosionDuration);
                yield return new WaitForEndOfFrame();
            }

            _vignette.intensity.value = 0f;
            _vignette.enabled.value = false;
        }
    }
}