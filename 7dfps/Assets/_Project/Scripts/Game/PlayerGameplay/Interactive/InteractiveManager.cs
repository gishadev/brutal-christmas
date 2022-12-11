namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class InteractiveManager : IInteractiveManager
    {
        private IInteractive _currentInteractive;
        
        public void EquipInteractive(IInteractive interactive)
        {
            _currentInteractive = interactive;
            interactive.gameObject.SetActive(false);
        }
    }
}