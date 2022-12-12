namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface IInteractiveManager
    {
        InteractiveData CurrentInteractive { get; }
        void TakePickable(IPickable interactivePickable);
    }
}