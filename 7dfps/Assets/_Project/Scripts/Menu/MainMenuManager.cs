using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.fpsjam.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CameraEnter _cameraEnter;
        [SerializeField] private GameObject rootGUI;
        [SerializeField] private GameObject[] guiScreens;

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
            SceneManager.LoadScene(Constants.GAME_SCENE);
        }

        public void OnClick_Quit()
        {
            Application.Quit();
        }

        public void OnClick_EnableScreen(int screenIndex)
        {
            foreach (var screen in guiScreens) screen.SetActive(false);

            guiScreens[screenIndex].SetActive(true);
        }
    }
}