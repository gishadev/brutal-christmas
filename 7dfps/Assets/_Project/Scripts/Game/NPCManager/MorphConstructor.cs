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
            IMorph morph = npc.Morph;

            for (int i = 0; i < npc.transform.childCount; i++)
            {
                if (npc.transform.GetChild(i).TryGetComponent(out morph))
                    break;
            }

            if (morph == null)
            {
                var randomPrefab = _npcData.Morphs[Random.Range(0, _npcData.Morphs.Length)];
                var obj = _diContainer.InstantiatePrefab(randomPrefab);
                obj.transform.SetParent(npc.transform);
                obj.TryGetComponent(out morph);
            }

            return morph;
        }
    }
}