using UnityEngine;

namespace Gisha.fpsjam.Game.Core
{
    public interface IPunchable
    {
        void OnPunch(Vector3 punchDir, float forceMagnitude);
    }
}