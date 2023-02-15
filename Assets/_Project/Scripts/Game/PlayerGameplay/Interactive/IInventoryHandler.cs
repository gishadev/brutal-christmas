﻿using System;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IInventoryHandler
    {
        Slot EquippedSlot { get; }
        event Action<Slot> SlotEquipped;
        public event Action<Slot, InteractiveData> SlotContentUpdated;

        void Init();
        void Dispose();
        bool IsFull();
        void TakePickable(IPickable pickable);
        void ClearSlot(Slot slot);
    }
}