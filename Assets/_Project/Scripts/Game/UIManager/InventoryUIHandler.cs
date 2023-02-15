using System.Collections.Generic;
using Gisha.fpsjam.Game.PlayerGameplay.Interactive;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.UIManager
{
    public class InventoryUIHandler : MonoBehaviour
    {
        [SerializeField] private Transform slotsUIParent;
        [SerializeField] private RectTransform equippedHandler;

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
            _inventory.SlotEquipped += OnSlotEquip;
        }

        private void OnDisable()
        {
            _inventory.SlotContentUpdated -= OnContentUpdate;
            _inventory.SlotEquipped -= OnSlotEquip;
        }

        private void OnSlotEquip(Slot slot)
        {
            equippedHandler.anchoredPosition = _slotsUIHandlers[slot.Index].RectTransform.anchoredPosition;
        }

        private void OnContentUpdate(Slot slot, InteractiveData data)
        {
            if (data == null)
                _slotsUIHandlers[slot.Index].ClearContent();
            else
                _slotsUIHandlers[slot.Index].ChangeContent(data.IconSprite);
        }
    }
}