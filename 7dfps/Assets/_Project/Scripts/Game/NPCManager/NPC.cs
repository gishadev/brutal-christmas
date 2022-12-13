using System;
using Gisha.fpsjam.Game.Core;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public abstract class NPC : MonoBehaviour, INPC, IPunchable
    {
        [Inject] private IMorphConstructor _morphConstructor;

        public IMorph Morph { get; private set; }
        public INPCMovement Movement { get; private set; }
        public INPCCelebrationHandler CelebrationHandler { get; private set; }
        public INPCAnimatorController NPCAnimator { get; private set; }

        public bool IsDied { get; private set; }

        public event Action Died;

        protected StateMachine _stateMachine;
        protected IState _startState;
        
        private Collider _collider;

        private void Awake()
        {
            Init();
            
            IsDied = false;
            _collider = GetComponent<Collider>();
            Movement = GetComponent<INPCMovement>();
            CelebrationHandler = new NPCCelebrationHandler();
        }

        private void Start()
        {
            InitStateMachine();
        }

        private void Init()
        {
            Morph = _morphConstructor.CreateRandomMorph(this);
            NPCAnimator = Morph.gameObject.GetComponent<INPCAnimatorController>();
        }

        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            Morph.Ragdoll.Enable();
            Morph.Ragdoll.AddForce(punchDir * forceMagnitude, ForceMode.Impulse);
            _collider.enabled = false;

            Died?.Invoke();
            IsDied = true;
        }

        public abstract void InitStateMachine();
    }
}