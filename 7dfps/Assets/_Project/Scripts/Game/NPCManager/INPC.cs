using System;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPC
    {
        GameObject gameObject { get; }
        Transform transform { get; }

        IMorph Morph { get; }
        INPCMovement Movement { get; }
        INPCCelebrationHandler CelebrationHandler { get; }
        INPCAnimatorController NPCAnimator { get; }

        bool IsDied { get; }
        bool IsRespawnable { get; }
        event Action<INPC> Died;
        event Action Respawned;

        void Die();
        void Respawn();
        
        abstract void InitStateMachine();
    }
}