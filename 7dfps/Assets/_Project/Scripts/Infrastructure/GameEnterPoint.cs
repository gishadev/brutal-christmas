using Gisha.fpsjam.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.fpsjam.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        private async void Init()
        {
            SceneManager.LoadScene(Constants.GAME_SCENE);
        }
    }
}