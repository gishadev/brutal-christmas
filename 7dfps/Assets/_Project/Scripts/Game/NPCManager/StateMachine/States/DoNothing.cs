using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class DoNothing : IState
    {
        private float _startTime;

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

        public float GetTime()
        {
            return Time.time - _startTime;
        }
    }
}