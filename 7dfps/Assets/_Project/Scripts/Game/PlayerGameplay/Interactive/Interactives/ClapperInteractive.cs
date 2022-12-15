using Gisha.fpsjam.Game.NPCManager;
using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class ClapperInteractive : Interactive, ICelebrative
    {
        [SerializeField] private float raycastDst = 2f;
        [SerializeField] private float raycastRadius = 2f;
        [Space] [SerializeField] private float emittingCelebrationPower = 0.25f;

        public float EmittingCelebrationPower => emittingCelebrationPower;
        
        public override void Use()
        {
            Debug.Log("Boom!");

            EmitCelebration(0.25f);
        }

        public void EmitCelebration(float power)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var hits = Physics.SphereCastAll(ray, raycastRadius, raycastDst);

            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out INPC npc))
                    continue;

                npc.CelebrationHandler.Celebrate(EmittingCelebrationPower);
            }
        }
    }
}