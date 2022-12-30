using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Optimisation
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "ScriptableObjects/PoolData")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private List<PoolObject> poolObjects = new List<PoolObject>();
        
        public List<PoolObject> PoolObjects => poolObjects;
    }
}