using System.Collections;
using Gisha.Effects.Audio;
using Gisha.fpsjam.Game.Core;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class LegPunchHandler : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerData _playerData;
        private IAudioManager _audioManager;
        
        private Animator _animator;
        private Camera _cam;
        private bool _isPunching;

        [Inject]
        private void Construct(IInputService inputService, PlayerData playerData, IAudioManager audioManager)
        {
            _inputService = inputService;
            _playerData = playerData;
            _audioManager = audioManager;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _cam = Camera.main;
            _inputService.LegPunchButtonDown += DoPunch;
        }

        private void OnDisable()
        {
            _inputService.LegPunchButtonDown -= DoPunch;
        }

        private void DoPunch()
        {
            StartCoroutine(PunchRoutine());
        }

        private IEnumerator PunchRoutine()
        {
            if (_isPunching)
                yield break;

            _isPunching = true;

            _animator.SetTrigger("Punch");

            yield return new WaitForSeconds(_playerData.PunchDelay);
            _isPunching = false;
        }

        private void PunchRaycast()
        {
            var screenPointRay = _cam.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(_cam.transform.position, screenPointRay.direction);
            var hits = Physics.SphereCastAll(ray, _playerData.PunchRaycastRadius, _playerData.PunchRaycastDst);

            Debug.DrawRay(ray.origin, ray.direction * _playerData.PunchRaycastDst, Color.red, 0.5f);
            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out IPunchable punchable))
                    continue;

                punchable.OnPunch(screenPointRay.direction.normalized, _playerData.LegPunchForce);
            }
        }

        private void OnPunchAnimationEvent()
        {
            PunchRaycast();
            _audioManager.Play("leg_punch", AudioType.SFX);
        }
    }
}