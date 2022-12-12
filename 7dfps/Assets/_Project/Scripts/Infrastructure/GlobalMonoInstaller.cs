using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Game.PlayerGameplay;
using Gisha.fpsjam.Game.PlayerGameplay.Interactive;
using Zenject;

namespace Gisha.fpsjam.Infrastructure
{
    public class GlobalMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<IPlayerManager>().To<PlayerManager>().AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
            Container.Bind<IMorphConstructor>().To<MorphConstructor>().AsSingle().NonLazy();

            Container.BindInterfacesTo<InventoryHandler>().AsSingle().NonLazy();
        }
    }
}