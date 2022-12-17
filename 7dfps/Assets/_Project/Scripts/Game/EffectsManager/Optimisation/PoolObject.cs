using UnityEngine;
using System.Linq;

namespace Gisha.Optimisation
{
    [System.Serializable]
    public class PoolObject : IPoolObject
    {
        [SerializeField] private string name;
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private PoolObjectType poolObjectType;

        public string Name => name;
        public GameObject Prefab => IsVariative ? prefabs[Random.Range(0, prefabs.Length)] : prefabs[0];
        public bool IsVariative => prefabs.Length > 1;
        public int[] InstanceIds => prefabs.Select(x => x.GetInstanceID()).ToArray();
        public PoolObjectType PoolType => poolObjectType;

        public PoolObject(GameObject prefab, PoolObjectType poolObjectType)
        {
            prefabs = new GameObject[1];
            prefabs[0] = prefab;

            name = prefabs[0].name;
            this.poolObjectType = poolObjectType;
        }
    }

    public enum PoolObjectType
    {
        VFX,
        SFX
    }
}