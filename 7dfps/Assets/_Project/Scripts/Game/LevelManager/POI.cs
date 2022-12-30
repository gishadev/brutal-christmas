using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    public class POI : MonoBehaviour
    {
        [SerializeField] private NPCType npcType;

        public NPCType NPCType => npcType;
    }

    public enum NPCType
    {
        WalkingNPC,
        StandingNPC
    }
}