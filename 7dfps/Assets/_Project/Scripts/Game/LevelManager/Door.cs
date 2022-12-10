using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    public class Door : MonoBehaviour, IPunchable
    {
        public void OnPunch(Vector3 punchDir)
        {
            gameObject.SetActive(false);
        }
    }
}