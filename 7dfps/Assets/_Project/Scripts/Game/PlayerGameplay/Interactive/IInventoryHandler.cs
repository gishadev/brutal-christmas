using System;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IInventoryHandler
    {
        Slot EquippedSlot { get; }
        event Action<Slot> SlotEquipped;
        public event Action<int, IInteractive> SlotContentUpdated;

        bool IsFull();
        void TakeInteractive(IInteractive interactive);
    }
}