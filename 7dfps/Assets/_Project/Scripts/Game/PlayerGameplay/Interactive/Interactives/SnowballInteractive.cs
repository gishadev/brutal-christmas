using Gisha.Effects.Audio;
using Gisha.fpsjam.Utilities;
using UnityEngine;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class SnowballInteractive : Interactive
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float throwForce = 10f;

        [Inject] private DiContainer _diContainer;
        [Inject] private IAudioManager _audioManager;

        private LayerMask _allExceptPlayer;

        private void Awake()
        {
            _allExceptPlayer = ~ LayerMask.NameToLayer(Constants.PLAYER_MASK_NAME);
        }

        public override void Use()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var direction = ray.direction;

            var projectile = _diContainer.InstantiatePrefab(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            if (Physics.Raycast(ray, out var hitInfo, 1000f, _allExceptPlayer))
                direction = (hitInfo.point - transform.position).normalized;

            var rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(direction.normalized * throwForce, ForceMode.Impulse);

            _audioManager.Play("throw", AudioType.SFX);
        }
    }
}