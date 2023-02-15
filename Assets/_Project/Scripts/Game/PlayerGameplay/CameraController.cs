using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [Inject] private PlayerData _playerData;
        [Inject] private IInputService _inputService;

        private float _desiredX, _xRotation;
        private Rigidbody _playerRb;
        private float _yOffset;

        private void Awake()
        {
            _playerRb = playerTransform.GetComponent<Rigidbody>();
            _yOffset = transform.position.y - playerTransform.position.y;
        }

        void Update()
        {
            if (!_inputService.IsWorking)
                return;

            transform.position = playerTransform.transform.position + Vector3.up * _yOffset;
            Look();
        }

        private void Look()
        {
            float mouseX = Input.GetAxis("Mouse X") * _playerData.Sensitivity * Time.fixedDeltaTime *
                           _playerData.SensMultiplier;
            float mouseY = Input.GetAxis("Mouse Y") * _playerData.Sensitivity * Time.fixedDeltaTime *
                           _playerData.SensMultiplier;

            //Find current look rotation
            Vector3 rot = transform.localRotation.eulerAngles;
            _desiredX = rot.y + mouseX;

            //Rotate, and also make sure we dont over- or under-rotate.
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            //Perform the rotations
            transform.localRotation = Quaternion.Euler(_xRotation, _desiredX, 0f);
            _playerRb.MoveRotation(Quaternion.Euler(0, _desiredX, 0));
        }
    }
}