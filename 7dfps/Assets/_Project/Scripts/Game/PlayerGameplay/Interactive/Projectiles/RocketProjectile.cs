using System.Collections;
using Gisha.fpsjam.Game.NPCManager;
using Gisha.fpsjam.Utilities;
using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive.Projectiles
{
    public class RocketProjectile : MonoBehaviour, IProjectile, ICelebrative
    {
        [Header("General")] [SerializeField] private float flySpeed = 3f;
        [SerializeField] private float lifeTime = 5f;
        [Header("Explosion")] [SerializeField] private float celebrationPower = 0.55f;
        [SerializeField] private float explosionRadius = 5f;
        [Header("Raycast")] [SerializeField] private float raycastRadius = 0.5f;
        [SerializeField] private float raycastDst = 1f;


        public float EmittingCelebrationPower => celebrationPower;
        private LayerMask _allExceptPlayer;


        private void Awake()
        {
            _allExceptPlayer = ~ LayerMask.NameToLayer(Constants.PLAYER_MASK_NAME);
            StartCoroutine(LifeTimeRoutine());
        }

        private void Update()
        {
            Fly();

            var ray = new Ray(transform.position, transform.up);
            if (Physics.SphereCast(ray, raycastRadius, raycastDst, _allExceptPlayer))
                Explode();
        }

        private void Fly()
        {
            transform.Translate(transform.up * (flySpeed * Time.deltaTime), Space.World);
            Debug.DrawRay(transform.position, transform.up * 10f, Color.magenta, 0.1f);
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

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, raycastRadius);
            Gizmos.DrawRay(transform.position, transform.up * raycastDst);
        }
    }
}