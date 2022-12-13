using Gisha.fpsjam.Game.NPCManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.CelebrationManager
{
    public class CelebrationManager : ICelebrationManager
    {
        [Inject] private INPCSpawner _npcSpawner;

        public float CelebrationLevel { get; private set; }

        public void Init()
        {
            foreach (var npc in _npcSpawner.NPCs) 
                npc.CelebrationHandler.Celebrated += OnCelebrate;
        }
        
        public void OnCelebrate(float power)
        {
            CelebrationLevel += power;
            Debug.Log($"Celebration level: {CelebrationLevel}");
        }
    }
}