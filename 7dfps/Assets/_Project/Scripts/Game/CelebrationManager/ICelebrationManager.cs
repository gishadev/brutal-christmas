﻿namespace Gisha.fpsjam.Game.CelebrationManager
{
    public interface ICelebrationManager
    {
        float CelebrationLevel { get; }
        void OnCelebrate();
    }
}