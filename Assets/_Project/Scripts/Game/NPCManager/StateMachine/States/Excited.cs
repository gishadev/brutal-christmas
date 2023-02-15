using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Excited : IState
    {
        private Animator _animator;
        
        public Excited(Animator animator)
        {
            _animator = animator;
        }
        
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}