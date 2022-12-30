using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(Animator))]
    public class Ragdoll : MonoBehaviour, IRagdoll
    {
        [SerializeField] private Rigidbody hipsRb;

        private Rigidbody[] _rbs;
        private Animator _animator;

        private Vector3[] _localPositions;
        private Quaternion[] _localRotations;
        private Transform[] _localTransforms;


        private void Awake()
        {
            _rbs = GetComponentsInChildren<Rigidbody>();
            _animator = GetComponent<Animator>();
            SaveTransforms();
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
            LoadTransforms();
        }

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            foreach (var rb in _rbs)
                rb.AddForce(force, forceMode);
        }

        private void SaveTransforms()
        {
            _localTransforms = hipsRb.GetComponentsInChildren<Transform>();
            _localPositions = new Vector3[_localTransforms.Length];
            _localRotations = new Quaternion[_localTransforms.Length];

            for (var i = 0; i < _localPositions.Length; i++)
            {
                _localPositions[i] = _localTransforms[i].localPosition;
                _localRotations[i] = _localTransforms[i].localRotation;
            }
        }

        private void LoadTransforms()
        {
            for (var i = 0; i < _localPositions.Length; i++)
            {
                _localTransforms[i].localPosition = _localPositions[i];
                _localTransforms[i].localRotation = _localRotations[i];
            }
        }
    }

    public interface IRagdoll
    {
        public void Enable();
        public void Disable();
        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force);
    }
}