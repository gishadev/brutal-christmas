using UnityEngine;

namespace Gisha.Optimisation
{
    public interface IPoolObject
    {
        string Name { get; }
        GameObject Prefab { get; }
        bool IsVariative { get; }
        int[] InstanceIds { get; }
    }
}