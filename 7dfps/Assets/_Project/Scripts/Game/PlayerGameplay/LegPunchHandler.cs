using System.Collections;
using Gisha.fpsjam.Game.Core;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class LegPunchHandler : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerData _playerData;
        
        private Animator _animator;
        private Camera _cam;
        private bool _isPunching;

        [Inject]
        private void Construct(IInputService inputService, PlayerData playerData)
        {
            _inputService = inputService;
            _playerData = playerData;
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

        private void OnPunchAnimationEvent() => PunchRaycast();
    }
}