using Gisha.Effects.Audio;
using Gisha.Effects.VFX;
using Gisha.fpsjam.Game.CelebrationManager;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using Gisha.fpsjam.Game.PlayerGameplay.Interactive;
using Gisha.fpsjam.Infrastructure;
using Gisha.fpsjam.Utilities;
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
        private IVFXManager _vfxManager;
        private IAudioManager _audioManager;
        private SignalBus _signalBus;
        private IInventoryHandler _inventory;

        private ITimer _timer;

        [Inject]
        public void Construct(IInputService inputService, IPlayerManager playerManager, INPCSpawner npcSpawner,
            ICelebrationManager celebrationManager, ITimer timer, IVFXManager vfxManager, SignalBus signalBus,
            IAudioManager audioManager, IInventoryHandler inventoryHandler)
        {
            _inputService = inputService;
            _playerManager = playerManager;
            _npcSpawner = npcSpawner;
            _celebrationManager = celebrationManager;
            _timer = timer;
            _vfxManager = vfxManager;
            _signalBus = signalBus;
            _audioManager = audioManager;
            _inventory = inventoryHandler;
        }

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _playerManager.Init(player);
            _npcSpawner.Init();
            _celebrationManager.Init();
            _vfxManager.Init();
            _inputService.Init();
            _audioManager.Init();
            _inventory.Init();
            
            _celebrationManager.Celebrated += OnCelebrated;
        }

        private void Start()
        {
            _timer.Start();
            _signalBus.Fire<GameStartedSignal>();
        }

        private void OnDisable()
        {
            _celebrationManager.Celebrated -= OnCelebrated;
            _npcSpawner.Dispose();
            _inputService.Dispose();
            _inventory.Dispose();
        }

        private void Update()
        {
            _inputService.Update();
            _timer.Tick();
        }

        private void Win()
        {
            Debug.Log("YO, you win.");

            _timer.Pause();

            if (!PlayerPrefs.HasKey(Constants.BEST_TIME_KEY) ||
                _timer.CurrentTime < PlayerPrefs.GetFloat(Constants.BEST_TIME_KEY))
                PlayerPrefs.SetFloat(Constants.BEST_TIME_KEY, _timer.CurrentTime);

            _signalBus.Fire<WinSignal>();
        }

        private void Lose()
        {
            _timer.Pause();

            _signalBus.Fire<LoseSignal>();
        }

        private void OnCelebrated(float lastCelebrationPower)
        {
            if (_celebrationManager.CelebrationLevel >= _celebrationManager.MaxCelebrationLevel)
                Win();
        }
    }
}