using Gisha.fpsjam.Game.Core;
using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    public class Window : MonoBehaviour, IPunchable
    {
        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            gameObject.SetActive(false);
        }
    }
}