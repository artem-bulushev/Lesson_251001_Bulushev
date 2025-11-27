using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    public class Raycast : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance = 50;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Explode _explodePrefab;
        //[SerializeField] private GameObject _gameObject;
        //[SerializeField] private Animator _animator;
        //[SerializeField] private Collider _collider;

        //private void Start()
        //{
        //    _animator.enabled = true;
        //    _collider.enabled = true;
        //}
        private void Update()
        {
            //_animator.enabled = true;
            //_collider.enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastObjects();
            }
        }

        private void RaycastObjects()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _distance, _layerMask))
            {
                var explosion = Instantiate<Explode>(_explodePrefab, hit.point, Quaternion.identity);
                explosion.ExplodeObj();
                //_animator.enabled = false;
                //_collider.enabled = false;
                //_gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction*_distance);
        }
    }
}