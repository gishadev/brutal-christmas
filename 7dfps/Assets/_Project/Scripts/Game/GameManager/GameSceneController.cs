using System;
using Gisha.fpsjam.Game.CelebrationManager;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.GameManager
{
    public class GameSceneController : MonoBehaviour
    {
        private IInputService _inputService;
        private IPlayerManager _playerManager;
        private INPCSpawner _npcSpawner;
        private ICelebrationManager _celebrationManager;

        [Inject]
        public void Construct(IInputService inputService, IPlayerManager playerManager, INPCSpawner npcSpawner,
            ICelebrationManager celebrationManager)
        {
            _inputService = inputService;
            _playerManager = playerManager;
            _npcSpawner = npcSpawner;
            _celebrationManager = celebrationManager;
        }

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _playerManager.Init(player);
            _npcSpawner.Init();
            _celebrationManager.Init();

            _celebrationManager.Celebrated += OnCelebrated;
        }

        private void OnDisable()
        {
            _celebrationManager.Celebrated -= OnCelebrated;
        }

        private void Update()
        {
            _inputService.Update();
        }

        private void OnCelebrated(float lastCelebrationPower)
        {
            if (_celebrationManager.CelebrationLevel >= _celebrationManager.MaxCelebrationLevel)
                Win();
        }
        
        private void Win()
        {
            Debug.Log("YO, you win.");
        }

        private void Lose()
        {
        }
    }
}