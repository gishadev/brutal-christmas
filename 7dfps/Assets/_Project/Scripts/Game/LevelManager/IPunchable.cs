using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    public interface IPunchable
    {
        void OnPunch(Vector3 punchDir);
    }
}