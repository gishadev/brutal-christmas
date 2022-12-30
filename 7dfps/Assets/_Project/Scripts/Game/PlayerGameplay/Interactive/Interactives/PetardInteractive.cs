using Gisha.Effects.Audio;
using UnityEngine;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class PetardInteractive : Interactive
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float throwForce = 10f;

        [Inject] private DiContainer _diContainer;
        [Inject] private IAudioManager _audioManager;

        public override void Use()
        {
            var projectile = _diContainer.InstantiatePrefab(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            var direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            var rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
            
            _audioManager.Play("throw", AudioType.SFX);
        }
    }
}