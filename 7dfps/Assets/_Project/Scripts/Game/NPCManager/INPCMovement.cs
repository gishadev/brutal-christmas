using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCMovement
    {
        Transform[] PointsOfInterest { get; }
        void MoveToDestination(Vector3 destination);
    }
}