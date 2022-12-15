using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class PetardInteractive : Interactive
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float throwForce = 10f;

        [Inject] private DiContainer _diContainer;

        public override void Use()
        {
            var projectile = _diContainer.InstantiatePrefab(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            var direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            var rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }
}