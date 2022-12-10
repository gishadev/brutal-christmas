using Gisha.fpsjam.Game.Core;
using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    public class Door : MonoBehaviour, IPunchable
    {
        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            gameObject.SetActive(false);
        }
    }
}