using Gisha.fpsjam.Infrastructure;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class PopupsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject winPopup, losePopup;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<WinSignal>(ShowWinPopup);
            _signalBus.Subscribe<LoseSignal>(ShowLosePopup);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<WinSignal>(ShowWinPopup);
            _signalBus.Unsubscribe<LoseSignal>(ShowLosePopup);
        }

        private void ShowLosePopup() => ShowPopup(losePopup);
        private void ShowWinPopup() => ShowPopup(winPopup);

        private void ShowPopup(GameObject popup)
        {
            HidePopups();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            popup.SetActive(true);
        }

        private void HidePopups()
        {
            winPopup.SetActive(false);
            losePopup.SetActive(false);
        }

        public void OnClick_Restart()
        {
            SceneManager.LoadScene(Constants.GAME_SCENE);
        }

        public void OnClick_ReturnToMenu()
        {
            SceneManager.LoadScene(Constants.MENU_SCENE);
        }
    }
}