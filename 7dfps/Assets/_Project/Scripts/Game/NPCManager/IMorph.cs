using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface IMorph
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        
        IRagdoll Ragdoll { get; }
    }
}