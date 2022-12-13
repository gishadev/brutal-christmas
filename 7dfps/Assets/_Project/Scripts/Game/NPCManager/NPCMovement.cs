﻿using Gisha.fpsjam.Game.LevelManager;
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
            _pointsOfInterest = FindObjectsOfType<POI>();
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

        private void OnDied()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }
}