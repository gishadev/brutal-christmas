using UnityEngine;

namespace Gisha.fpsjam.Game.GameManager
{
    public class Timer : ITimer
    {
        public bool IsTicking { get; private set; }
        public float CurrentTime { get; private set; }

        public void Start()
        {
            Reset();
            IsTicking = true;
        }

        public void Pause()
        {
            IsTicking = false;
        }

        public void Resume()
        {
            IsTicking = true;
        }

        public void Reset()
        {
            CurrentTime = 0f;
        }

        public void Tick()
        {
            if (!IsTicking)
                return;

            CurrentTime += Time.deltaTime;
        }
    }
}