using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Gisha.Optimisation
{
    public class PoolManager : IPoolManager
    {
        [Inject] private PoolData _poolData;

        private Dictionary<IPoolObject, List<GameObject>> _objectsByPoolObject =
            new Dictionary<IPoolObject, List<GameObject>>();

        private Dictionary<IPoolObject, Transform> _parentByPoolObject = new Dictionary<IPoolObject, Transform>();


        private Transform _parent;

        public void Init()
        {
            _parent = new GameObject("[POOL]").transform;

            _objectsByPoolObject = new Dictionary<IPoolObject, List<GameObject>>();
            _parentByPoolObject = new Dictionary<IPoolObject, Transform>();

            InitializePools(_poolData.PoolObjects);
        }

        protected bool TryEmit(string name, PoolObjectType poolType, out GameObject emittedObj)
        {
            var objects = _poolData.PoolObjects.Where(x => x.PoolType == poolType);

            var poolObj = objects.FirstOrDefault(x => x.Name == name);
            var prefab = poolObj.Prefab;
            emittedObj = null;

            if (poolObj.Equals(null))
                return false;

            var po = GetOrCreatePoolObject(prefab);
            if (_objectsByPoolObject.TryGetValue(po, out var sceneObjectsList))
            {
                if (sceneObjectsList.Any(x => !x.activeInHierarchy))
                {
                    emittedObj = ActivateAvailableObject(sceneObjectsList);
                    return true;
                }
            }
            else
            {
                _objectsByPoolObject.Add(po, new List<GameObject>());
                CreateObjectParent(po);
            }

            emittedObj = InstantiateNewObject(prefab, po);
            return true;
        }

        private void InitializePools(List<PoolObject> poolObjects)
        {
            foreach (var po in poolObjects)
            {
                _objectsByPoolObject.Add(po, new List<GameObject>());
                CreateObjectParent(po);
            }
        }

        #region Object Instantiating

        private GameObject InstantiateNewObject(GameObject prefab,
            IPoolObject po)
        {
            Transform parent = _parentByPoolObject[po];

            GameObject createdObject = Object.Instantiate(prefab, parent);
            _objectsByPoolObject[po].Add(createdObject);

            return createdObject;
        }

        private GameObject ActivateAvailableObject(List<GameObject> sceneObjectsList)
        {
            GameObject objectToActivate;
            if (sceneObjectsList.Count > 1)
            {
                List<GameObject> unactiveObjects = sceneObjectsList.Where(x => !x.activeInHierarchy).ToList();
                objectToActivate = unactiveObjects.ElementAtOrDefault(new Random().Next() % unactiveObjects.Count());
            }
            else
                objectToActivate = sceneObjectsList.FirstOrDefault(x => !x.activeInHierarchy);

            objectToActivate.SetActive(true);

            return objectToActivate;
        }

        #endregion

        private IPoolObject GetOrCreatePoolObject(GameObject prefab)
        {
            var prefabId = prefab.GetInstanceID();
            var index = _poolData.PoolObjects.FindIndex(x => x.InstanceIds.Contains(prefabId));

            if (index == -1)
            {
                var newPO = new PoolObject(prefab, PoolObjectType.VFX);
                _poolData.PoolObjects.Add(newPO);
                index = _poolData.PoolObjects.Count - 1;

                Debug.LogFormat("New Pool Object was created with name {0}.", newPO.Name);
            }

            return _poolData.PoolObjects[index];
        }

        private void CreateObjectParent(IPoolObject poKey)
        {
            var name = string.Format("pool_{0}", poKey.Name);
            var parent = new GameObject(name);
            parent.transform.SetParent(_parent);

            _parentByPoolObject.Add(poKey, parent.transform);
        }
    }
}