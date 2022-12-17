using System;
using System.Collections;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive.Projectiles
{
    public class SnowballProjectile : MonoBehaviour, ICelebrative
    {
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float celebrationPower = 0.1f;
        [SerializeField] private float raycastRadius = 0.5f;

        public float EmittingCelebrationPower => celebrationPower;

        private void Awake()
        {
            StartCoroutine(LifeTimeRoutine());
        }

        public void EmitCelebration(float power)
        {
            var hits = Physics.SphereCastAll(transform.position, raycastRadius, Vector3.up, 0f);

            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out INPC npc))
                    continue;

                npc.CelebrationHandler.Celebrate(power);
            }
        }

        private IEnumerator LifeTimeRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_MASK_NAME))
                return;

            if (other.TryGetComponent(out INPC npc))
                EmitCelebration(EmittingCelebrationPower);

            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, raycastRadius);
        }
    }
}