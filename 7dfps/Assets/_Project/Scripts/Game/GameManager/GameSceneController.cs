using System;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.GameManager
{
    public class GameSceneController : MonoBehaviour
    {
        private IInputService _inputService;
        private IPlayerManager _playerManager;

        [Inject]
        public void Construct(IInputService inputService, IPlayerManager playerManager)
        {
            _inputService = inputService;
            _playerManager = playerManager;
        }

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _playerManager.Init(player);
        }

        private void Update()
        {
            _inputService.Update();
        }
    }
}