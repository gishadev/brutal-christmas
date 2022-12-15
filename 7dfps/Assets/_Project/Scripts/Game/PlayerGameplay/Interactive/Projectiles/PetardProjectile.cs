using System.Collections;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive.Projectiles
{
    public class PetardProjectile : MonoBehaviour, IProjectile
    {
        [Header("General")]
        [SerializeField] private float lifeTime = 5f;
        [Header("Explosion")] [SerializeField] private float celebrationPower = 0.55f;
        [SerializeField] private float explosionRadius = 5f;
        
        public float EmittingCelebrationPower => celebrationPower;

        private void Awake()
        {
            StartCoroutine(LifeTimeRoutine());
        }
        
        private void Explode()
        {
            EmitCelebration(celebrationPower);
            Destroy(gameObject);
        }

        public void EmitCelebration(float power)
        {
            var hits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.up, 0f);

            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out INPC npc))
                    continue;

                npc.CelebrationHandler.Celebrate(EmittingCelebrationPower);
            }
        }

        private IEnumerator LifeTimeRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            Explode();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}