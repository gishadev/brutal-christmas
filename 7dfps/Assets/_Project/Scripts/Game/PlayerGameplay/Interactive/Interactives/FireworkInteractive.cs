using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class FireworkInteractive : Interactive, ICelebrative
    {
        public float EmittingCelebrationPower { get; }

        public override void Use()
        {
            Debug.Log("Pew!");
            EmitCelebration(0.1f);
        }


        public void EmitCelebration(float power)
        {
        }
    }
}