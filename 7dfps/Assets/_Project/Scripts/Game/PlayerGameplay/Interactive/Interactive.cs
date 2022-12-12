using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public abstract class Interactive : MonoBehaviour, IInteractive
    {
        [SerializeField] protected InteractiveData interactiveData;

        public InteractiveData InteractiveData => interactiveData;
        
        public abstract void Use();
    }
}