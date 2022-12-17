using System;
using Gisha.Effects.Audio;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class StandingNPC : NPC
    {
        [SerializeField] private float chanceOfThinking = 25f;

        [Inject] private IAudioManager _audioManager;

        public override void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var standing = new Standing(NPCAnimator, chanceOfThinking);
            var thinking = new Thinking(this, _audioManager);
            var startCelebration = new StartCelebration(this, EMOTION_STATE.EXCITED,
                EMOTION_STATE.HAPPY, EMOTION_STATE.TERRIFIED);
            var emotioning = new Emotioning(this);
            var die = new Die(this, _audioManager);

            At(emotioning, standing, CelebrationAnimationFinished);
            At(standing, thinking, () => standing.IsThinking());

            At(die, standing, () => !IsDied);

            At(startCelebration, emotioning, () => true);
            Aat(startCelebration, () => CelebrationHandler.IsCelebration);
            Aat(die, () => IsDied);

            _stateMachine.SetState(standing);
            _startState = standing;

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