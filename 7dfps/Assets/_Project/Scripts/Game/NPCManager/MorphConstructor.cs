using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class MorphConstructor : IMorphConstructor
    {
        private NPCData _npcData;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(NPCData npcData, DiContainer container)
        {
            _npcData = npcData;
            _diContainer = container;
        }

        public IMorph CreateRandomMorph(INPC npc)
        {
            var randomPrefab = _npcData.Morphs[Random.Range(0, _npcData.Morphs.Length)];

            IMorph morph = npc.Morph;
            if (morph == null)
            {
                var obj = _diContainer.InstantiatePrefab(randomPrefab);
                obj.transform.SetParent(npc.transform);
            }

            return morph;
        }
    }
}