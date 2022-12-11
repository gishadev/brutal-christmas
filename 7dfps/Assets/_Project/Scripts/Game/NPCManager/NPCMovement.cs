using UnityEngine;
using UnityEngine.AI;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(NavMeshAgent), typeof(INPC))]
    public class NPCMovement : MonoBehaviour, INPCMovement
    {
        [SerializeField] private Transform[] pointsOfInterest;
        
        private NavMeshAgent _agent;
        private INPC _npc;

        public Transform[] PointsOfInterest => pointsOfInterest;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _npc = GetComponent<INPC>();
        }

        private void Start()
        {
            _agent.enabled = true;
            _npc.Died += OnDied;
        }
        
        private void OnDisable()
        {
            _npc.Died -= OnDied;
        }

        public void MoveToDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }
        
        private void OnDied()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }
}