namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class PlayerManager : IPlayerManager
    {
        public IPlayer Player { get; private set; }

        public void Init(IPlayer player)
        {
            Player = player;
        }
    }
}