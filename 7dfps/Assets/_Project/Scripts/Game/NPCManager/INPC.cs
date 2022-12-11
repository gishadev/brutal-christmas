using System;
using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPC
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        IMorph Morph { get; }
        event Action Died;
    }
}