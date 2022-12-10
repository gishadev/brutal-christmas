using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class NPC : MonoBehaviour, INPC
    {
        [Inject] private IMorphConstructor _morphConstructor;

        public IMorph Morph { get; private set; }

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            Morph = _morphConstructor.CreateRandomMorph(this);
        }
    }
}