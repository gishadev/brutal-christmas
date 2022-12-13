using System.Collections.Generic;

namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCSpawner
    {
        void Init();
        void SpawnAllEnemies();

        List<INPC> NPCs { get; }
    }
}