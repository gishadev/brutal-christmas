using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Infrastructure
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private NPCData npcData;

        public override void InstallBindings()
        {
            Container.BindInstances(playerData, npcData);
        }
    }
}