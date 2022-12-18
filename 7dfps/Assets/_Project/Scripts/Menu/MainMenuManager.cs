using Gisha.Effects.Audio;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CameraEnter _cameraEnter;
        [SerializeField] private GameObject rootGUI;
        [SerializeField] private GameObject[] guiScreens;

        [Inject] private IAudioManager _audioManager;

        private void Start()
        {
            rootGUI.SetActive(false);
        }

        private void OnEnable()
        {
            _cameraEnter.AnimationCompleted += OnCameraCompleted;
        }

        private void OnDisable()
        {
            _cameraEnter.AnimationCompleted -= OnCameraCompleted;
        }

        private void OnCameraCompleted()
        {
            rootGUI.SetActive(true);
        }

        public void OnClick_Play()
        {
            _audioManager.Play("click_2", AudioType.SFX);
            SceneManager.LoadScene(Constants.GAME_SCENE);
        }

        public void OnClick_ClearPrefs()
        {
            _audioManager.Play("click_2", AudioType.SFX);
            PlayerPrefs.DeleteAll();
        }

        public void OnClick_Quit()
        {
            _audioManager.Play("click_1", AudioType.SFX);
            Application.Quit();
        }

        public void OnClick_EnableScreen(int screenIndex)
        {
            _audioManager.Play("click_1", AudioType.SFX);
            foreach (var screen in guiScreens) screen.SetActive(false);

            guiScreens[screenIndex].SetActive(true);
        }
    }
}