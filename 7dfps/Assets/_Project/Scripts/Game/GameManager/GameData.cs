using UnityEngine;

namespace Gisha.fpsjam.Game.GameManager
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("NPC")]
        [SerializeField] private int maxWalkingNPCCount = 20;
        [SerializeField] private GameObject walkingNPCPrefab;
        [SerializeField] private GameObject standingNPCPrefab;
        [SerializeField] private float maxCelebrationLevel = 100f;
        
        public int MaxWalkingNpcCount => maxWalkingNPCCount;
        public GameObject StandingNPCPrefab => standingNPCPrefab;
        public GameObject WalkingNPCPrefab => walkingNPCPrefab;
        public float MaxCelebrationLevel => maxCelebrationLevel;
    }
}