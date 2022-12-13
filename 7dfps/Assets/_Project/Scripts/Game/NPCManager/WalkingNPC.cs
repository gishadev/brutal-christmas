using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkingNPC : NPC
    {
        [SerializeField] private float chanceOfThinking = 25f;
        [SerializeField] private float thinkingTime = 3f;

        public override void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var idle = new Standing(NPCAnimator, chanceOfThinking);
            var thinking = new Thinking(NPCAnimator);
            var randomWalk = new WalkToRandomPOI(Movement, NPCAnimator);
            var die = new Die();

            At(randomWalk, idle, DestinationSucceeded);
            At(idle, randomWalk, () => !idle.IsThinking());
            At(idle, thinking, () => idle.IsThinking());
            At(thinking, randomWalk, DelayFinished);

            Aat(die, () => IsDied);

            _stateMachine.SetState(randomWalk);
            _startState = randomWalk;

            bool DestinationSucceeded()
            {
                return (transform.position - randomWalk.Destination).sqrMagnitude < 1f;
            }

            bool DelayFinished() => idle.GetTime() > thinkingTime;

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}