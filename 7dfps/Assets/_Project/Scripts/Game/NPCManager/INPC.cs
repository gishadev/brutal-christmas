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
        event Action Died;

        abstract void InitStateMachine();
    }
}