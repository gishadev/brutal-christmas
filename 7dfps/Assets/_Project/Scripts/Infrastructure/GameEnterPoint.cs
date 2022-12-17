using Gisha.Effects.Audio;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        [Inject] private IAudioManager _audioManager;

        private void Awake()
        {
            Init();
        }

        private async void Init()
        {
            _audioManager.Init();
            _audioManager.Play("music", AudioType.Music);
            SceneManager.LoadScene(Constants.MENU_SCENE);
        }
    }
}