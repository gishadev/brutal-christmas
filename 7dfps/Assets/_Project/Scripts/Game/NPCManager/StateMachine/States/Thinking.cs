﻿using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Thinking : IState
    {
        private INPCAnimatorController _npcAnimator;
        private float _startTime;


        public Thinking(INPCAnimatorController npcAnimator)
        {
            _npcAnimator = npcAnimator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
            _npcAnimator.SetEmotion(EMOTION_STATE.THINKING);
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