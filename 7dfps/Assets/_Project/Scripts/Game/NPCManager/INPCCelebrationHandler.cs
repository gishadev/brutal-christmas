using System;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCCelebrationHandler
    {
        float CelebrationAccumulatedPower { get; }
        event Action Celebrated;
        void Celebrate(float power);
    }
}