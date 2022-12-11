using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class DoNothing : IState
    {
        private float _startTime;

        private Animator _animator;
        
        public DoNothing(Animator animator)
        {
            _animator = animator;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
            _animator.SetBool("IsWalking", false);
        }

        public void OnExit()
        {
        }

        public float GetTime()
        {
            return Time.time - _startTime;
        }
    }
}