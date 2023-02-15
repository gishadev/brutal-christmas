using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IInteractive
    {
        Transform transform { get; }
        GameObject gameObject { get; }
        InteractiveData InteractiveData { get; }
        
        void Use();
    }
}