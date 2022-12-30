using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Morph : MonoBehaviour, IMorph
    {
        public IRagdoll Ragdoll { get; private set; }

        private void Start()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            Ragdoll = GetComponent<IRagdoll>();
            Ragdoll.Disable();
        }
    }
}