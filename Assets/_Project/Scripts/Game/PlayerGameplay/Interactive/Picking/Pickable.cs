using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class Pickable : MonoBehaviour, IPickable
    {
        [SerializeField] private InteractiveData interactiveData;

        public InteractiveData InteractiveData => interactiveData;
        public Mesh Mesh { get; private set; }
        public Outline Outline { get; private set; }

        private void Awake()
        {
            Mesh = GetComponent<MeshFilter>().mesh;
            Outline = GetComponent<Outline>();
        }
    }
}