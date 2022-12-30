using Gisha.fpsjam.Game.GameManager;
using Gisha.fpsjam.Utilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class WinPopup : MonoBehaviour
    {
        [Inject] private ITimer _timer;

        [SerializeField] private TMP_Text currentTimeText;
        [SerializeField] private TMP_Text bestTimeText;

        private void OnEnable()
        {
            currentTimeText.text = TimeConverter.ConvertTime(_timer.CurrentTime);
            bestTimeText.text = TimeConverter.ConvertTime(PlayerPrefs.GetFloat(Constants.BEST_TIME_KEY));
        }
    }
}