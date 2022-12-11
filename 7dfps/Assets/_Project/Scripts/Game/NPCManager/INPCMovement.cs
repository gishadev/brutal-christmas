using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCMovement
    {
        POI[] PointsOfInterest { get; }
        void MoveToDestination(Vector3 destination);
    }
}