using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public abstract class Interactive : MonoBehaviour, IInteractive
    {
        [SerializeField] protected InteractiveData interactiveData;

        public InteractiveData InteractiveData => interactiveData;

        public abstract void Use();

        [ContextMenu("Save Offsets")]
        private void SaveOffsets()
        {
            interactiveData.OffsetPosition = transform.localPosition;
            interactiveData.OffsetRotation = transform.localRotation;
            interactiveData.Scale = transform.localScale;

            Debug.Log("Offsets saved!");
        }
    }
}