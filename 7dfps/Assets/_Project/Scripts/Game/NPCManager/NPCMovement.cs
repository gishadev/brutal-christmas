using System.Linq;
using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;
using UnityEngine.AI;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(NavMeshAgent), typeof(INPC))]
    public class NPCMovement : MonoBehaviour, INPCMovement
    {
        private NavMeshAgent _agent;
        private INPC _npc;

        public POI[] PointsOfInterest => _pointsOfInterest;

        private POI[] _pointsOfInterest;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _npc = GetComponent<INPC>();
            _pointsOfInterest = FindObjectsOfType<POI>()
                .Where(x => x.NPCType == NPCType.WalkingNPC)
                .ToArray();
        }

        private void Start()
        {
            _agent.enabled = true;
            _npc.Died += OnDied;
            _npc.Respawned += OnRespawned;
        }

        private void OnDisable()
        {
            _npc.Died -= OnDied;
            _npc.Respawned -= OnRespawned;
        }

        public void MoveToDestination(Vector3 destination)
        {
            if (_agent.enabled)
            {
                _agent.isStopped = false;
                _agent.SetDestination(destination);
            }
        }

        public void Stop()
        {
            if (_agent.enabled)
                _agent.isStopped = true;
        }

        private void OnDied(INPC npc)
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }

        private void OnRespawned()
        {
            _agent.enabled = true;
            _agent.isStopped = false;
        }
    }
}