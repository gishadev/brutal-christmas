using Gisha.fpsjam.Game.GameManager;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class TimerUIElement : MonoBehaviour
    {
        [Inject] private ITimer _timer;

        private TMP_Text textObj;

        private void Awake()
        {
            textObj = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            if (!_timer.IsTicking)
                return;

            UpdateTimeUI(_timer.CurrentTime);
        }

        private void UpdateTimeUI(float lastTime)
        {
            var minutes = Mathf.Floor(lastTime / 60);
            var seconds = (lastTime % 60);
            var fraction = (lastTime * 1000);
            fraction %= 1000;

            textObj.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        }
    }
}