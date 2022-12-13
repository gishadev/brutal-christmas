using System;
using Gisha.fpsjam.Game.NPCManager;
using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class PetardInteractive : Interactive, ICelebrative
    {
        [SerializeField] private float raycastDst = 2f;
        [SerializeField] private float raycastRadius = 2f;
        [Space] [SerializeField] private float emittingCelebrationPower = 0.25f;


        public float EmittingCelebrationPower => emittingCelebrationPower;

        private Camera _cam;
        private Vector2 _screenCenter;

        private void Awake()
        {
            _cam = Camera.main;
            _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        public override void Use()
        {
            Debug.Log("Boom!");
        }
        
        public void EmitCelebration(float power)
        {
            var ray = _cam.ScreenPointToRay(_screenCenter);

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