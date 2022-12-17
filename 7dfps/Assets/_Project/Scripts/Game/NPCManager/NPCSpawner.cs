using System.Collections.Generic;
using System.Threading.Tasks;
using Gisha.fpsjam.Game.GameManager;
using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class NPCSpawner : INPCSpawner
    {
        public List<INPC> NPCs => _npcs;

        private GameData _gameData;
        private DiContainer _diContainer;

        private List<INPC> _npcs = new List<INPC>();
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

        public void Dispose()
        {
            foreach (var npc in _npcs)
                npc.Died -= OnNPCDied;
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
                var npc = SpawnEnemy(_gameData.NPCPrefab, rPOI);
                npc.Died += OnNPCDied;
                POIs.Remove(rPOI);
            }
        }

        private INPC SpawnEnemy(GameObject prefab, POI poi)
        {
            var npcObj = _diContainer.InstantiatePrefab(prefab);
            npcObj.transform.position = poi.transform.position;
            npcObj.transform.SetParent(_parent);

            var npc = npcObj.GetComponent<INPC>();
            _npcs.Add(npc);
            return npc;
        }

        private async void OnNPCDied(INPC npc)
        {
            if (!npc.IsRespawnable)
                return;

            await Task.Delay(5000);
            npc.Respawn();
            npc.transform.position = _points[Random.Range(0, _points.Length)].transform.position;
        }
    }
}