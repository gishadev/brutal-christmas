using System;
using System.Linq;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InventoryHandler : IInventoryHandler, IInitializable, IDisposable
    {
        [Inject] private IInputService _inputService;

        public event Action<Slot> SlotEquipped;
        public Slot EquippedSlot => _slots[_equippedIndex];
        private Slot[] _slots = new Slot[Constants.MAX_INTERACTIVE_SLOTS];

        private int _equippedIndex = 0;

        public void Initialize()
        {
            _inputService.NumberButtonDown += OnNumberButtonDown;
            _inputService.MouseScroll += OnMouseScroll;
        }

        public void Dispose()
        {
            _inputService.NumberButtonDown -= OnNumberButtonDown;
            _inputService.MouseScroll -= OnMouseScroll;
        }

        public void TakeInteractive(IInteractive interactive)
        {
            if (IsFull())
                return;

            int freeSlotIndex = 0;
            for (int i = 0; i < _slots.Length; i++)
                if (_slots[i].Interactive == null || _slots[i].Interactive.Equals(null))
                {
                    freeSlotIndex = i;
                    break;
                }

            _slots[freeSlotIndex].Interactive = interactive;

            string str = "";
            for (int i = 0; i < _slots.Length; i++)
            {
                str += $"{i + 1}. ";

                if (_slots[i].Interactive == null || _slots[i].Interactive.Equals(null))
                    str += "NULL \n";
                else
                    str += $"{_slots[i].Interactive.gameObject.name} \n";
            }

            Debug.Log(str);
        }

        private void OnMouseScroll(float mouseYDelta)
        {
            int index = _equippedIndex;

            if (mouseYDelta > 0f)
                index++;
            if (mouseYDelta < 0f)
                index--;

            if (index > _slots.Length - 1)
                index = 0;

            if (index < 0)
                index = _slots.Length - 1;

            _equippedIndex = index;
            SlotEquipped?.Invoke(EquippedSlot);
        }

        private void OnNumberButtonDown(int index)
        {
            _equippedIndex = index;
            SlotEquipped?.Invoke(EquippedSlot);
        }

        public bool IsFull() => _slots.All(x => x.Interactive != null);
    }

    public struct Slot
    {
        public IInteractive Interactive;
    }
}