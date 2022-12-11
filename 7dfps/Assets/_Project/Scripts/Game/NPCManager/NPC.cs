﻿using System;
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
        public event Action Died;

        protected StateMachine _stateMachine;
        protected IState _startState;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            Movement = GetComponent<INPCMovement>();
            Init();
            InitStateMachine();
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

            Died?.Invoke();
        }

        public abstract void InitStateMachine();
    }
}