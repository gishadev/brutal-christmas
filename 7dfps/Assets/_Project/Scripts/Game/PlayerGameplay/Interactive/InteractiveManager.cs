using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InteractiveManager : IInteractiveManager
    {
        [Inject] private IInventoryHandler _inventory;

        public InteractiveData CurrentInteractive => _inventory.EquippedSlot.InteractiveData;

        private int _equppedIndex;

        public void TakePickable(IPickable interactivePickable)
        {
            if (_inventory.IsFull())
                return;

            _inventory.TakePickable(interactivePickable);
            Object.Destroy(interactivePickable.gameObject);
        }
    }
}