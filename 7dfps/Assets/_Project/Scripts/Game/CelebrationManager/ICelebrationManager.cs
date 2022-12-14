using System;

namespace Gisha.fpsjam.Game.CelebrationManager
{
    public interface ICelebrationManager
    {
        float CelebrationLevel { get; }
        float MaxCelebrationLevel { get; }
        void OnCelebrate(float celebrationPower);
        event Action<float> Celebrated;
        void Init();
    }
}