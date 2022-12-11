namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IInteractiveManager
    {
        IInteractive CurrentInteractive { get; }
        void TakeInteractive(IInteractive interactive);
    }
}