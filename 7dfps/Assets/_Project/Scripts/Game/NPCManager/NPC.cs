using Gisha.fpsjam.Game.Core;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class NPC : MonoBehaviour, INPC, IPunchable
    {
        [Inject] private IMorphConstructor _morphConstructor;

        public IMorph Morph { get; private set; }

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            Init();
        }

        public void Init()
        {
            Morph = _morphConstructor.CreateRandomMorph(this);
        }

        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            Morph.Ragdoll.Enable();
            Morph.Ragdoll.AddForce(punchDir * forceMagnitude, ForceMode.Impulse);
            _collider.enabled = false;
        }
    }
}