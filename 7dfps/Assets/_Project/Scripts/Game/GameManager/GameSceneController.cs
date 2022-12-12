using System;
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

        [Inject]
        public void Construct(IInputService inputService, IPlayerManager playerManager, INPCSpawner npcSpawner)
        {
            _inputService = inputService;
            _playerManager = playerManager;
            _npcSpawner = npcSpawner;
        }

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _playerManager.Init(player);
            _npcSpawner.Init();
        }

        private void Update()
        {
            _inputService.Update();
        }
    }
}