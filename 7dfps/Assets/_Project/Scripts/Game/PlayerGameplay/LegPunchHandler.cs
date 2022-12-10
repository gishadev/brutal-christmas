using System.Collections;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class LegPunchHandler : MonoBehaviour
    {
        [Header("General")] [SerializeField] private float punchDelay = 1.5f;
        [SerializeField] private float punchForce = 25f;

        [Header("Raycast")] [SerializeField] private float raycastDst = 5f;
        [SerializeField] private LayerMask whatIsPunchable;

        [Inject] private IInputService _inputService;

        private Camera _cam;
        private bool _isPunching;

        private void Awake()
        {
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
            PunchRaycast();

            yield return new WaitForSeconds(punchDelay);
            _isPunching = false;
        }

        private void PunchRaycast()
        {
            var screenPointRay = _cam.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(transform.position, screenPointRay.direction);
            if (Physics.Raycast(ray, out var hitInfo, raycastDst, whatIsPunchable))
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastDst, Color.red, 0.5f);

                if (hitInfo.collider == null)
                    return;

                if (!hitInfo.collider.TryGetComponent(out Rigidbody rb))
                    return;

                Debug.DrawRay(ray.origin, ray.direction * raycastDst, Color.green, 1.5f);
                rb.AddForce(screenPointRay.direction.normalized * punchForce, ForceMode.Impulse);
            }
        }
    }
}