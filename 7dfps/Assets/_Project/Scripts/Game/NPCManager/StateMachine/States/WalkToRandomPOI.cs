using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkToRandomPOI : IState
    {
        private INPCMovement _npcMovement;
        public Vector3 Destination { get; private set; }

        private Animator _animator;

        public WalkToRandomPOI(INPCMovement movement, Animator animator)
        {
            _npcMovement = movement;
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            var randPoint = _npcMovement.PointsOfInterest[Random.Range(0, _npcMovement.PointsOfInterest.Length)];
            Destination = randPoint.transform.position;
            _npcMovement.MoveToDestination(Destination);
            _animator.SetBool("IsWalking", true);
        }

        public void OnExit()
        {
        }
    }
}