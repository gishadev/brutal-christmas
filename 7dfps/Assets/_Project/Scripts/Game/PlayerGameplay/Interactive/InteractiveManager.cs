using System;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InteractiveManager : IInteractiveManager, IInitializable, IDisposable
    {
        [Inject] private IInputService _inputService;
        [Inject] private IInventoryHandler _inventory;

        public IInteractive CurrentInteractive => _inventory.EquippedSlot.Interactive;

        private int _equppedIndex;

        public void TakeInteractive(IInteractive interactive)
        {
            if (_inventory.IsFull())
                return;

            _inventory.TakeInteractive(interactive);
            interactive.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _inputService.LMBDown += OnLMBDown;
        }

        public void Dispose()
        {
            _inputService.LMBDown -= OnLMBDown;
        }

        private void OnLMBDown()
        {
            if (CurrentInteractive == null || CurrentInteractive.Equals(null))
                return;

            CurrentInteractive.Use();
            Object.Destroy(CurrentInteractive.gameObject);
        }
    }
}