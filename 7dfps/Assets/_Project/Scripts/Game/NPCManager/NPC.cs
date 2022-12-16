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

        public event Action<INPC> Died;
        public event Action Respawned;

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
            Die();
            Morph.Ragdoll.AddForce(punchDir * forceMagnitude, ForceMode.Impulse);
        }

        public void Die()
        {
            IsDied = true;
            _collider.enabled = false;

            Morph.Ragdoll.Enable();

            Died?.Invoke(this);
        }

        public void Respawn()
        {
            IsDied = false;
            _collider.enabled = true;
            Morph.Ragdoll.Disable();
            CelebrationHandler.Reset();
            
            Respawned?.Invoke();
        }

        public abstract void InitStateMachine();
    }
}