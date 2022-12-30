using Gisha.Effects.Audio;
using Gisha.fpsjam.Game.GameManager;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using Gisha.Optimisation;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Infrastructure
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private NPCData npcData;
        [SerializeField] private GameData gameData;
        [SerializeField] private PoolData poolData;
        [SerializeField] private AudioData audioData;

        public override void InstallBindings()
        {
            Container.BindInstances(playerData, npcData, gameData, poolData, audioData);
        }
    }
}