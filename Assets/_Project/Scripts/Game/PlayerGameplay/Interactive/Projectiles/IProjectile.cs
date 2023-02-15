using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive.Projectiles
{
    public interface IProjectile
    {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}