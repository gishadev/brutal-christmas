using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Standing : IState
    {
        private float _startTime;

        private INPCAnimatorController _animator;
        private float _thinkingChance;

        public Standing(INPCAnimatorController animator, float thinkingChance)
        {
            _animator = animator;
            _thinkingChance = thinkingChance;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
            _animator.SetMovementState(MOVEMENT_STATE.IDLE);
        }

        public void OnExit()
        {
        }

        public float GetTime()
        {
            return Time.time - _startTime;
        }

        public bool IsThinking()
        {
            return Random.Range(0f, 100f) < _thinkingChance;
        }
    }
}