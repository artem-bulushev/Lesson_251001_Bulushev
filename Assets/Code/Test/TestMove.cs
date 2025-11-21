using System;
using UnityEngine;

namespace AI
{
    public sealed class TestMove : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        private UnityEngine.AI.NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        }

        private void Update()
        {
            if (_target != null)
            {
                _agent.SetDestination(_target.position);
            }
        }
    }
}
