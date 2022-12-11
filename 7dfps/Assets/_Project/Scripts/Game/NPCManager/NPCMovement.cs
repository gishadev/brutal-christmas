using UnityEngine;
using UnityEngine.AI;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(NavMeshAgent), typeof(INPC))]
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private Transform moveTo;

        private NavMeshAgent _agent;
        private INPC _npc;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _npc = GetComponent<INPC>();
        }

        private void Start()
        {
            _agent.enabled = true;
            _agent.SetDestination(moveTo.position);
        }

        private void OnEnable()
        {
            _npc.Died += OnDied;
        }

        private void OnDisable()
        {
            _npc.Died -= OnDied;
        }

        private void OnDied()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }
}