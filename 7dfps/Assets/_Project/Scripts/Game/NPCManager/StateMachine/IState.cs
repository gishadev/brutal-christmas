namespace Gisha.fpsjam.Game.NPCManager
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}