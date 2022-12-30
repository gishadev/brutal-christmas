using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkToRandomPOI : IState
    {
        private INPCMovement _npcMovement;
        public Vector3 Destination { get; private set; }

        private INPCAnimatorController _animator;

        public WalkToRandomPOI(INPCMovement movement, INPCAnimatorController animator)
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
            _animator.SetMovementState(MOVEMENT_STATE.WALK);
        }

        public void OnExit()
        {
            _npcMovement.Stop();
        }
    }
}