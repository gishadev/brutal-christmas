using System;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCCelebrationHandler
    {
        float CelebrationAccumulatedPower { get; }
        bool IsCelebration { get; set; }
        event Action<float> Celebrated;
        void Celebrate(float power);
    }
}