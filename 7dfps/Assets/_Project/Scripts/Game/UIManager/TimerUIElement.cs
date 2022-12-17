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
            textObj.text = TimeConverter.ConvertTime(lastTime);
        }
    }
}