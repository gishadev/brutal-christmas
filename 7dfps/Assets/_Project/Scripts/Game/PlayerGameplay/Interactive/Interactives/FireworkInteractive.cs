using Gisha.fpsjam.Utilities;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class FireworkInteractive : Interactive
    {
        [SerializeField] private GameObject projectilePrefab;

        [Inject] private DiContainer _diContainer;
        private LayerMask _allExceptPlayer;

        private void Awake()
        {
            _allExceptPlayer = ~ LayerMask.NameToLayer(Constants.PLAYER_MASK_NAME);
        }

        public override void Use()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var direction = ray.direction;

            if (Physics.Raycast(ray, out var hitInfo, 1000f, _allExceptPlayer))
                direction = (hitInfo.point - transform.position).normalized;

            var projectile = _diContainer.InstantiatePrefab(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation =
                Quaternion.LookRotation(direction) * Quaternion.Euler(Vector3.right * 90f);
        }
    }
}