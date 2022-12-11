using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class TestInteractive : MonoBehaviour, IInteractive
    {
        public void Use()
        {
            Debug.Log("Interactive Used!");
        }
    }
}