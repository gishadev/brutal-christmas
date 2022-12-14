using UnityEngine;

namespace Gisha.fpsjam.Game.GameManager
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("NPC")]
        [SerializeField] private int maxNPCCount = 4;
        [SerializeField] private GameObject npcPrefab;
        [SerializeField] private float maxCelebrationLevel = 100f;
        
        public int MaxNpcCount => maxNPCCount;
        public GameObject NPCPrefab => npcPrefab;
        public float MaxCelebrationLevel => maxCelebrationLevel;
    }
}