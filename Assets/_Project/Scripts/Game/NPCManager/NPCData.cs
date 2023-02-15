using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    [CreateAssetMenu(fileName = "NPCData", menuName = "ScriptableObjects/NPCData")]
    public class NPCData : ScriptableObject
    {
        [SerializeField] private GameObject[] morphs;

        public GameObject[] Morphs => morphs;
    }
}