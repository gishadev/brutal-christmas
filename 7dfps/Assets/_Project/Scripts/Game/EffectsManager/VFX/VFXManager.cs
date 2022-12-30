using UnityEngine;
using Gisha.Optimisation;

namespace Gisha.Effects.VFX
{
    public class VFXManager : PoolManager, IVFXManager
    {
        public void EmitAt(string effectName, Vector3 position, Quaternion rotation)
        {
            if (!TryEmit(effectName, PoolObjectType.VFX, out var obj))
                return;

            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
    }
}