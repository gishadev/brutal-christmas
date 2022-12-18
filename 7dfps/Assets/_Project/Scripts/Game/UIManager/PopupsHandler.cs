using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Infrastructure;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class PopupsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject winPopup, losePopup, pausePopup;

        private SignalBus _signalBus;
        private IInputService _inputService;

        private bool _isPopupShowing;

        [Inject]
        private void Construct(SignalBus signalBus, IInputService inputService)
        {
            _signalBus = signalBus;
            _inputService = inputService;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<WinSignal>(ShowWinPopup);
            _signalBus.Subscribe<LoseSignal>(ShowLosePopup);
            _inputService.EscapeButtonDown += OnEscapeButtonDown;
        }


        private void OnDisable()
        {
            _signalBus.Unsubscribe<WinSignal>(ShowWinPopup);
            _signalBus.Unsubscribe<LoseSignal>(ShowLosePopup);
            _inputService.EscapeButtonDown -= OnEscapeButtonDown;
        }

        private void ShowLosePopup() => ShowPopup(losePopup);
        private void ShowWinPopup() => ShowPopup(winPopup);

        private void ShowPausePopup()
        {
            _signalBus.Fire<PauseSignal>();
            ShowPopup(pausePopup);
        }

        private void ShowPopup(GameObject popup)
        {
            HidePopups();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            popup.SetActive(true);

            _isPopupShowing = true;
        }

        private void HidePopups()
        {
            winPopup.SetActive(false);
            losePopup.SetActive(false);
            pausePopup.SetActive(false);
            _isPopupShowing = false;
        }

        public void OnClick_Continue()
        {
            _signalBus.Fire<ResumeSignal>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            HidePopups();
        }

        public void OnClick_Restart()
        {
            SceneManager.LoadScene(Constants.GAME_SCENE);
        }

        public void OnClick_ReturnToMenu()
        {
            SceneManager.LoadScene(Constants.MENU_SCENE);
        }

        private void OnEscapeButtonDown()
        {
            if (_isPopupShowing)
                return;

            ShowPausePopup();
        }
    }
}