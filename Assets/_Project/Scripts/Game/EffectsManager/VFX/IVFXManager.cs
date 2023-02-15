using UnityEngine;

namespace Gisha.Effects.VFX
{
    public interface IVFXManager
    {
        void EmitAt(string effectName, Vector3 position, Quaternion rotation);
        void Init();
    }
}