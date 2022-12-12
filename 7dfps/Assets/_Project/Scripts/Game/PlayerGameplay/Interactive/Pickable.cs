using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class Pickable : MonoBehaviour, IPickable
    {
        [SerializeField] private InteractiveData interactiveData;

        public InteractiveData InteractiveData => interactiveData;
    }
}