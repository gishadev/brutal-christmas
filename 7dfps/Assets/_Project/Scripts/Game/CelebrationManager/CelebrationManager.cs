using System;
using Gisha.fpsjam.Game.GameManager;
using Gisha.fpsjam.Game.NPCManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.CelebrationManager
{
    public class CelebrationManager : ICelebrationManager
    {
        [Inject] private INPCSpawner _npcSpawner;
        [Inject] private GameData _gameData;
        public event Action<float> Celebrated;
        public float CelebrationLevel { get; private set; }
        public float MaxCelebrationLevel => _gameData.MaxCelebrationLevel;

        public void Init()
        {
            CelebrationLevel = 0f;
            foreach (var npc in _npcSpawner.NPCs)
                npc.CelebrationHandler.Celebrated += OnCelebrate;
        }

        public void OnCelebrate(float power)
        {
            CelebrationLevel += power;
            Debug.Log($"Celebration level: {CelebrationLevel}");
            Celebrated?.Invoke(power);
        }
    }
}