using System;
using System.Linq;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Utilities;
using Zenject;
using Object = UnityEngine.Object;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InventoryHandler : IInventoryHandler, IInitializable, IDisposable
    {
        [Inject] private IInputService _inputService;

        public event Action<Slot> SlotEquipped;
        public event Action<Slot, InteractiveData> SlotContentUpdated;

        public Slot EquippedSlot => _equippedSlot;
        private Slot[] _slots = new Slot[Constants.MAX_INTERACTIVE_SLOTS];

        private Slot _equippedSlot;
        
        public void Initialize()
        {
            _inputService.NumberButtonDown += OnNumberButtonDown;
            _inputService.MouseScroll += OnMouseScroll;

            for (int i = 0; i < _slots.Length; i++)
                _slots[i] = new Slot(i);

            _equippedSlot = _slots[0];
        }

        public void Dispose()
        {
            _inputService.NumberButtonDown -= OnNumberButtonDown;
            _inputService.MouseScroll -= OnMouseScroll;
        }

        public void TakePickable(IPickable pickable)
        {
            if (IsFull())
                return;

            Object.Destroy(pickable.gameObject);

            int freeSlotIndex = 0;
            for (int i = 0; i < _slots.Length; i++)
                if (_slots[i].InteractiveData == null || _slots[i].InteractiveData.Equals(null))
                {
                    freeSlotIndex = i;
                    break;
                }

            _slots[freeSlotIndex].InteractiveData = pickable.InteractiveData;
            SlotContentUpdated?.Invoke(_slots[freeSlotIndex], pickable.InteractiveData);
        }

        public void ClearSlot(Slot slot)
        {
            slot.InteractiveData = null;
            SlotContentUpdated?.Invoke(slot, null);
        }

        private void OnMouseScroll(float mouseYDelta)
        {
            int index = _equippedSlot.Index;

            if (mouseYDelta > 0f)
                index++;
            if (mouseYDelta < 0f)
                index--;

            if (index > _slots.Length - 1)
                index = 0;

            if (index < 0)
                index = _slots.Length - 1;

            _equippedSlot = _slots[index];
            SlotEquipped?.Invoke(_equippedSlot);
        }

        private void OnNumberButtonDown(int index)
        {
            _equippedSlot = _slots[index];
            SlotEquipped?.Invoke(_equippedSlot);
        }

        public bool IsFull() => _slots.All(x => x.InteractiveData != null);
    }

    public class Slot
    {
        public int Index => _index;
        public InteractiveData InteractiveData { get; set; }

        private int _index;

        public Slot(int index)
        {
            _index = index;
        }
    }
}