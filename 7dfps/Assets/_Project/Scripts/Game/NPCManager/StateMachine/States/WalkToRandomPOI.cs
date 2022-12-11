using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkToRandomPOI : IState
    {
        private INPCMovement _npcMovement;
        public Vector3 Destination { get; private set; }


        public WalkToRandomPOI(INPCMovement movement)
        {
            _npcMovement = movement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            var randPoint = _npcMovement.PointsOfInterest[Random.Range(0, _npcMovement.PointsOfInterest.Length)];
            Destination = randPoint.transform.position;
            _npcMovement.MoveToDestination(Destination);
        }

        public void OnExit()
        {
        }
    }
}