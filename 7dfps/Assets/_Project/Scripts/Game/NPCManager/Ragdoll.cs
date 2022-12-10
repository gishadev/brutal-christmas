using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(Animator))]
    public class Ragdoll : MonoBehaviour, IRagdoll
    {
        private Rigidbody[] _rbs;
        private Animator _animator;

        private void Awake()
        {
            _rbs = GetComponentsInChildren<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        [ContextMenu("Enable Ragdoll")]
        public void Enable()
        {
            foreach (var rb in _rbs) rb.isKinematic = false;

            _animator.enabled = false;
        }

        [ContextMenu("Disable Ragdoll")]
        public void Disable()
        {
            foreach (var rb in _rbs) rb.isKinematic = true;

            _animator.enabled = true;
        }

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            foreach (var rb in _rbs) 
                rb.AddForce(force, forceMode);
        }
    }

    public interface IRagdoll
    {
        public void Enable();
        public void Disable();
        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force);
    }
}