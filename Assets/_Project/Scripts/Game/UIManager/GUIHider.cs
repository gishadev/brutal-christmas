using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class GUIHider : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        private Canvas[] _canvases;
        private bool _isHidden;

        private void Awake()
        {
            _canvases = FindObjectsOfType<Canvas>();
            _inputService.HideUIButtonDown += OnHideUI;
        }

        private void OnHideUI()
        {
            _isHidden = !_isHidden;

            foreach (var canvas in _canvases)
                canvas.enabled = !_isHidden;
        }
    }
}