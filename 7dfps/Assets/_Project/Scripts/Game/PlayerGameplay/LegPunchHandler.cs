using System.Collections;
using Gisha.fpsjam.Game.InputManager;
using Gisha.fpsjam.Game.LevelManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class LegPunchHandler : MonoBehaviour
    {
        [Header("General")] [SerializeField] private float punchDelay = 1.5f;
        [SerializeField] private float punchForce = 25f;
        [SerializeField] private GameObject leg;

        [Header("Raycast")] [SerializeField] private float raycastDst = 5f;
        [SerializeField] private float raycastRadius = 0.6f;

        [Inject] private IInputService _inputService;

        private Camera _cam;
        private bool _isPunching;

        private void Awake()
        {
            _cam = Camera.main;
            _inputService.LegPunchButtonDown += DoPunch;
            leg.SetActive(false);
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
            PunchAnimation();

            yield return new WaitForSeconds(punchDelay);
            _isPunching = false;
        }

        private void PunchRaycast()
        {
            var screenPointRay = _cam.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(transform.position, screenPointRay.direction);
            var hits = Physics.SphereCastAll(ray, raycastRadius, raycastDst);

            Debug.DrawRay(ray.origin, ray.direction * raycastDst, Color.red, 0.5f);
            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out IPunchable punchable))
                    continue;

                punchable.OnPunch(screenPointRay.direction.normalized);
            }
        }

        private void PunchAnimation()
        {
            leg.SetActive(true);
            Invoke("DisableLeg", 0.3f);
        }

        private void DisableLeg()
        {
            leg.SetActive(false);
        }
    }
}