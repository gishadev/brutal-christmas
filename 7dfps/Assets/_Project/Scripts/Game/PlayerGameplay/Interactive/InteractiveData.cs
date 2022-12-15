using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    [CreateAssetMenu(fileName = "InteractiveData", menuName = "ScriptableObjects/InteractiveData")]
    public class InteractiveData : ScriptableObject
    {
        [Space] [SerializeField] private bool isSingleUse;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private GameObject prefab;
        [Space] [SerializeField] private Vector3 offsetPosition;
        [SerializeField] private Quaternion offsetRotation;
        [SerializeField] private Vector3 scale;

        public Sprite IconSprite => iconSprite;
        public GameObject Prefab => prefab;
        public bool IsSingleUse => isSingleUse;

        public Quaternion OffsetRotation
        {
            get => offsetRotation;
            set => offsetRotation = value;
        }

        public Vector3 OffsetPosition
        {
            get => offsetPosition;
            set => offsetPosition = value;
        }

        public Vector3 Scale
        {
            get => scale;
            set => scale = value;
        }
    }
}