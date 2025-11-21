using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        public int EnemySpeed = 3;
        private Transform _player;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Transform>(); 
        }

        private void Update()
        {
            _agent.SetDestination(_player.position);
        }
        public void DeactivateEnemy()
        {
            _agent.speed = 0;
        }
        public void ActivateEnemy()
        {
            _agent.speed = EnemySpeed;
        }
    }
}