using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Morph : MonoBehaviour, IMorph
    {
        private void Start()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}