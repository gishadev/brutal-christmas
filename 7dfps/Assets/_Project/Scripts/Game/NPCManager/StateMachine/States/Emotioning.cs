using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Emotioning : IState
    {
        private INPC _npc;
        private float _startTime;

        public Emotioning(INPC npc)
        {
            _npc = npc;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
        }

        public void OnExit()
        {
        }

        public float GetAnimationLength()
        {
            return _npc.NPCAnimator.GetCurrentAnimationLength();
        }

        public float GetTime()
        {
            return Time.time - _startTime;
        }
    }
}