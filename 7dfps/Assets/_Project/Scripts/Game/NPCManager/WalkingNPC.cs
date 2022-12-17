using System;
using Gisha.Effects.Audio;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class WalkingNPC : NPC
    {
        [SerializeField] private float chanceOfThinking = 25f;
        [SerializeField] private float thinkingTime = 3f;

        [Inject] private IAudioManager _audioManager;
        
        public override void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var standing = new Standing(NPCAnimator, chanceOfThinking);
            var thinking = new Thinking(this, _audioManager);
            var randomWalk = new WalkToRandomPOI(Movement, NPCAnimator);
            var startCelebration = new StartCelebration(this, EMOTION_STATE.EXCITED,
                EMOTION_STATE.HAPPY, EMOTION_STATE.TERRIFIED);
            var emotioning = new Emotioning(this);
            var die = new Die(this, _audioManager);

            At(randomWalk, standing, DestinationSucceeded);
            At(emotioning, standing, CelebrationAnimationFinished);
            At(standing, randomWalk, () => !standing.IsThinking());
            At(standing, thinking, () => standing.IsThinking());
            At(thinking, randomWalk, ThinkingDelayFinished);

            At(die, standing, () => !IsDied);

            At(startCelebration, emotioning, () => true);
            Aat(startCelebration, () => CelebrationHandler.IsCelebration);
            Aat(die, () => IsDied);

            _stateMachine.SetState(randomWalk);
            _startState = randomWalk;

            bool DestinationSucceeded() => (transform.position - randomWalk.Destination).sqrMagnitude < 1f;

            bool ThinkingDelayFinished() => thinking.GetTime() > thinkingTime;
            bool CelebrationAnimationFinished() => emotioning.GetTime() > emotioning.GetAnimationLength();

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        }


        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}
