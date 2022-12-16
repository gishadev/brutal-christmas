using UnityEngine;
using System.Linq;

namespace Gisha.Optimisation
{
    [System.Serializable]
    public class PoolObject : IPoolObject
    {
        [SerializeField] private string name;
        [SerializeField] private GameObject[] prefabs;

        public string Name => name;
        public GameObject Prefab => IsVariative ? prefabs[Random.Range(0, prefabs.Length)] : prefabs[0];
        public bool IsVariative => prefabs.Length > 1;
        public int[] InstanceIds => prefabs.Select(x => x.GetInstanceID()).ToArray();

        public PoolObject(GameObject _prefab, int _initCount = 1)
        {
            prefabs = new GameObject[1];
            prefabs[0] = _prefab;

            name = prefabs[0].name;
        }

        public PoolObject(GameObject[] _prefabs, int _initCount = 1)
        {
            prefabs = _prefabs;

            name = prefabs[0].name;
        }
    }
}