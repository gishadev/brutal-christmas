using System;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class NPCCelebrationHandler : INPCCelebrationHandler
    {
        public float CelebrationAccumulatedPower { get; private set; }
        public bool IsCelebration { get; set; }
        public event Action<float> Celebrated;

        public void Celebrate(float power)
        {
            IsCelebration = true;
            CelebrationAccumulatedPower += power;
            Celebrated?.Invoke(power);
        }
    }
}