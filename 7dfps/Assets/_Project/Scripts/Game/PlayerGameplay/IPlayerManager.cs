namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public interface IPlayerManager
    {
        IPlayer Player { get; }
        void Init(IPlayer player);
    }
}