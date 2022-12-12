using System;
using System.Collections.Generic;
using Gisha.fpsjam.Game.PlayerGameplay.Interactive;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class InventoryUIHandler : MonoBehaviour
    {
        [SerializeField] private Transform slotsUIParent;

        [Inject] private IInventoryHandler _inventory;

        private List<SlotUIHandler> _slotsUIHandlers = new List<SlotUIHandler>();

        private void Awake()
        {
            for (int i = 0; i < slotsUIParent.childCount; i++)
            {
                if (slotsUIParent.GetChild(i).TryGetComponent(out SlotUIHandler slotUI))
                    _slotsUIHandlers.Add(slotUI);
            }
        }

        private void OnEnable()
        {
            _inventory.SlotContentUpdated += OnContentUpdate;
        }

        private void OnDisable()
        {
            _inventory.SlotContentUpdated -= OnContentUpdate;
        }

        private void OnContentUpdate(int slotIndex, IInteractive interactiveContent)
        {
            _slotsUIHandlers[slotIndex].ChangeContent(interactiveContent.InteractiveData.IconSprite);
        }
    }
}