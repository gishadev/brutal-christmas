using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InteractiveEquipper : MonoBehaviour
    {
        [SerializeField] private float takeDst = 2f;
        [SerializeField] private float takeRadius = 0.25f;

        private IInputService _inputService;
        private IInteractiveManager _interactiveManager;

        private IInteractive _potentialInteractive;
        private Camera _cam;
        private LayerMask _layerMask;
        private Ray _screenPointRay;

        [Inject]
        private void Construct(IInputService inputService, IInteractiveManager interactiveManager)
        {
            _interactiveManager = interactiveManager;
            _inputService = inputService;
        }

        private void Awake()
        {
            _cam = Camera.main;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.INTERACTIVE_LAYER_NAME);

            _inputService.EquipButtonDown += OnEquipBtnDown;
        }

        private void OnDisable()
        {
            _inputService.EquipButtonDown -= OnEquipBtnDown;
        }

        private void Update()
        {
            _screenPointRay = _cam.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(_cam.transform.position, _screenPointRay.direction);

            if (Physics.SphereCast(ray, takeRadius, out var hitInfo, takeDst, _layerMask))
            {
                if (hitInfo.collider == null)
                    return;

                if (!hitInfo.collider.TryGetComponent(out _potentialInteractive))
                    return;

                Debug.Log($"Interactive detected!: {_potentialInteractive.gameObject.name}");
            }
        }

        private void OnEquipBtnDown()
        {
            if (_potentialInteractive == null)
                return;

            _interactiveManager.EquipInteractive(_potentialInteractive);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_screenPointRay.origin, takeRadius);
            Gizmos.DrawRay(_screenPointRay.origin, _screenPointRay.direction * takeDst);
        }
    }
}