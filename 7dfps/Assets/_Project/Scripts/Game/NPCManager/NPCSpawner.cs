using System.Collections.Generic;
using Gisha.fpsjam.Game.GameManager;
using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class NPCSpawner : INPCSpawner
    {
        private GameData _gameData;
        private DiContainer _diContainer;

        private POI[] _points;
        private Transform _parent;

        [Inject]
        private void Construct(GameData gameData, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _gameData = gameData;
        }

        public void Init()
        {
            _points = Object.FindObjectsOfType<POI>();
            _parent = new GameObject("[NPC]").transform;

            SpawnAllEnemies();
        }

        public void SpawnAllEnemies()
        {
            int count = _gameData.MaxNpcCount;
            if (count > _points.Length)
            {
                count = _points.Length;
                Debug.LogError("_gameData.MaxNpcCount is higher than points count.");
            }


            List<POI> POIs = new List<POI>(_points);
            for (int i = 0; i < count; i++)
            {
                var rPOI = POIs[Random.Range(0, POIs.Count)];
                SpawnEnemy(_gameData.NPCPrefab, rPOI);
                POIs.Remove(rPOI);
            }
        }

        private void SpawnEnemy(GameObject prefab, POI poi)
        {
            var npcObj = _diContainer.InstantiatePrefab(prefab);
            npcObj.transform.position = poi.transform.position;
            npcObj.transform.SetParent(_parent);
        }
    }
}