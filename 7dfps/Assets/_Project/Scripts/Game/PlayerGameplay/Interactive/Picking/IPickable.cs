using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IPickable
    {
        Transform transform { get; }
        GameObject gameObject { get; }
        InteractiveData InteractiveData { get; }
    }
}