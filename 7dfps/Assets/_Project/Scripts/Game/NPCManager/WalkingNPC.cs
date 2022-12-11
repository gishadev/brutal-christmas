using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkingNPC : NPC
    {
        [SerializeField] private float chanceOfStopping = 25f;
        [SerializeField] private float afterStopDelay = 1.5f;

        public override void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var idle = new DoNothing();
            var randomWalk = new WalkToRandomPOI(Movement);
            
            At(idle, randomWalk, DelayFinished);
            At(randomWalk, idle, RandomStop);
            At(randomWalk, randomWalk, DestinationSucceeded);
            
            _stateMachine.SetState(randomWalk);
            _startState = randomWalk;

            bool DestinationSucceeded() => (transform.position - randomWalk.Destination).sqrMagnitude < 5f;
            bool RandomStop() => DestinationSucceeded() && Random.Range(0, 100f) < chanceOfStopping;
            bool DelayFinished() => idle.GetTime() > afterStopDelay;

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}