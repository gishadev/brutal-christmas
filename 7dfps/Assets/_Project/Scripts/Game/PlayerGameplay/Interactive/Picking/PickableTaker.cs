using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class PickableTaker : MonoBehaviour
    {
        [SerializeField] private float takeDst = 2f;
        [SerializeField] private float takeRadius = 0.25f;

        private IInputService _inputService;
        private IInventoryHandler _inventoryHandler;

        private IPickable _potentialPickable;
        private Camera _cam;
        private LayerMask _layerMask;
        private Ray _screenPointRay;

        [Inject]
        private void Construct(IInputService inputService, IInventoryHandler inventoryHandler)
        {
            _inventoryHandler = inventoryHandler;
            _inputService = inputService;
        }

        private void Awake()
        {
            _cam = Camera.main;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.PICKABLE_LAYER_NAME);

            _inputService.PickButtonDown += OnPickBtnDown;
        }

        private void OnDisable()
        {
            _inputService.PickButtonDown -= OnPickBtnDown;
        }

        private void Update()
        {
            _screenPointRay = _cam.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(_cam.transform.position, _screenPointRay.direction);
            
            // Outline reseting.
            if (_potentialPickable != null && !_potentialPickable.Equals(null))
                _potentialPickable.Outline.enabled = false;

            if (Physics.SphereCast(ray, takeRadius, out var hitInfo, takeDst, _layerMask))
            {
                if (hitInfo.collider == null)
                    return;
                
                if (!hitInfo.collider.TryGetComponent(out _potentialPickable))
                    return;

                _potentialPickable.Outline.enabled = true;
            }
        }

        private void OnPickBtnDown()
        {
            if (_potentialPickable == null || _potentialPickable.Equals(null))
                return;

            _inventoryHandler.TakePickable(_potentialPickable);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_screenPointRay.origin, takeRadius);
            Gizmos.DrawRay(_screenPointRay.origin, _screenPointRay.direction * takeDst);
        }
    }
}