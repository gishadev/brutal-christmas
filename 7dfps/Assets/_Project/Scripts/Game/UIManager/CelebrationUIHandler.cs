using System;
using Gisha.fpsjam.Game.CelebrationManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class CelebrationUIHandler : MonoBehaviour
    {
        [Inject] private ICelebrationManager _celebrationManager;

        [SerializeField] private Image maskImg;


        private void OnEnable()
        {
            _celebrationManager.Celebrated += OnCelebrated;
        }

        private void OnDisable()
        {
            _celebrationManager.Celebrated -= OnCelebrated;
        }

        private void OnCelebrated(float lastCelebrationPower)
        {
            maskImg.fillAmount = _celebrationManager.CelebrationLevel / _celebrationManager.MaxCelebrationLevel;
        }
    }
}