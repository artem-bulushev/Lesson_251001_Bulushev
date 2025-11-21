using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;

namespace Code
{
    public class LightExplo : MonoBehaviour
    {
        [SerializeField] private AttackBah _attackBah;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartExplosion();
                _attackBah.StartExplosionEffect();
            }
        }

        private void StartExplosion()
        {
            if (_particleSystem.isPlaying==false)
            {
                _particleSystem.Play();
            }
        }
    }
}