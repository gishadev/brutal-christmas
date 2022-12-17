using Gisha.Effects.Audio;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Thinking : IState
    {
        private float _startTime;
        private IAudioManager _audioManager;
        private INPC _npc;

        public Thinking(INPC npc, IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _npc = npc;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
            _audioManager.EmitSpatial("npc_thinking", _npc.transform.position);
            _npc.NPCAnimator.SetEmotion(EMOTION_STATE.THINKING);
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