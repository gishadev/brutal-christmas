using Gisha.Effects.Audio;
using Gisha.Effects.VFX;
using Gisha.fpsjam.Game.CelebrationManager;
using Gisha.fpsjam.Game.GameManager;
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

            Container.DeclareSignal<WinSignal>();
            Container.DeclareSignal<LoseSignal>();
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<PauseSignal>();
            Container.DeclareSignal<ResumeSignal>();

            Container.Bind<IVFXManager>().To<VFXManager>().AsSingle().NonLazy();
            Container.Bind<IAudioManager>().To<AudioManager>().AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
            
            Container.Bind<IPlayerManager>().To<PlayerManager>().AsSingle().NonLazy();
            Container.Bind<IMorphConstructor>().To<MorphConstructor>().AsSingle().NonLazy();
            Container.Bind<INPCSpawner>().To<NPCSpawner>().AsSingle().NonLazy();
            Container.Bind<ICelebrationManager>().To<CelebrationManager>().AsSingle().NonLazy();
            Container.Bind<ITimer>().To<Timer>().AsSingle().NonLazy();
            Container.Bind<IInventoryHandler>().To<InventoryHandler>().AsSingle().NonLazy();
        }
    }
}