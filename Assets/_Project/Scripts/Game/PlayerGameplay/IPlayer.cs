using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public interface IPlayer
    {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}