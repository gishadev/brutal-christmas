using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InteractiveEquipper : MonoBehaviour
    {
        [SerializeField] private Transform handTrans;

        private IInteractive _currentInteractive;

        private IInputService _inputService;
        private IInventoryHandler _inventoryHandler;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer container, IInputService inputService, IInventoryHandler inventoryHandler)
        {
            _inputService = inputService;
            _inventoryHandler = inventoryHandler;
            _diContainer = container;
        }

        private void OnEnable()
        {
            _inputService.LMBDown += OnLMBDown;
            _inventoryHandler.SlotEquipped += OnSlotEquipped;
        }

        private void OnDisable()
        {
            _inputService.LMBDown -= OnLMBDown;
            _inventoryHandler.SlotEquipped -= OnSlotEquipped;
        }

        private void OnLMBDown()
        {
            if (_currentInteractive == null)
                return;

            _currentInteractive.Use();
        }

        private void OnSlotEquipped(Slot slot)
        {
            ClearInteractive();

            if (slot.InteractiveData == null)
                return;

            var interactive = _diContainer.InstantiatePrefab(slot.InteractiveData.Prefab)
                .GetComponent<IInteractive>();

            interactive.transform.SetParent(handTrans);
            interactive.transform.localPosition = Vector3.zero;
            //  interactive.transform.localScale = Vector3.zero;
            interactive.transform.localRotation = Quaternion.identity;

            _currentInteractive = interactive;
        }

        private void ClearInteractive()
        {
            if (_currentInteractive != null && !_currentInteractive.Equals(null))
            {
                Destroy(_currentInteractive.gameObject);
                _currentInteractive = null;
            }
        }
    }
}