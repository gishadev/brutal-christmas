using System;
using System.Collections;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerData _playerData;
        private IInputService _inputService;

        private bool _readyToJump = true;

        private Rigidbody _rb;
        private Player _player;

        [Inject]
        private void Construct(IInputService inputService, PlayerData playerData)
        {
            _inputService = inputService;
            _playerData = playerData;
        }

        void Awake()
        {
            _player = GetComponent<Player>();
            _rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            _inputService.JumpButtonDown += OnJumpButtonDown;
        }


        private void OnDisable()
        {
            _inputService.JumpButtonDown -= OnJumpButtonDown;
        }


        private void FixedUpdate()
        {
            Movement();
        }

        private void OnJumpButtonDown()
        {
            if (_readyToJump)
                Jump();
        }

        private void Movement()
        {
            //Extra gravity
            _rb.AddForce(Vector3.down * (Time.deltaTime * _playerData.GravityForce));

            Vector2 velRelativeToLook = FindVelRelativeToLook();

            //Counteract sliding and sloppy movement
            CounterMovement(_inputService.HorizontalInput, _inputService.VerticalInput, velRelativeToLook);

            //If sliding down a ramp, add force down so player stays grounded and also builds speed
            if (_player.IsSliding && _player.IsGrounded && _readyToJump)
            {
                _rb.AddForce(Vector3.down * (Time.deltaTime * 3000));
                return;
            }

            var maxSpeedMult = GetMaxSpeedMultiplier(velRelativeToLook);

            //Some multipliers
            float multiplier = 1f, multiplierV = 1f;

            // Movement in air
            if (!_player.IsGrounded)
            {
                multiplier = 0.5f;
                multiplierV = 0.5f;
            }

            // Movement while sliding
            if (_player.IsGrounded && _player.IsSliding) multiplierV = 0f;

            //Apply forces to move player
            _rb.AddForce(transform.forward *
                         (_inputService.VerticalInput * maxSpeedMult.y * _playerData.MoveSpeed * Time.deltaTime *
                          multiplier *
                          multiplierV));
            _rb.AddForce(transform.right *
                         (_inputService.HorizontalInput * maxSpeedMult.x * _playerData.MoveSpeed * Time.deltaTime *
                          multiplier));
        }

        private Vector2 GetMaxSpeedMultiplier(Vector2 velRelativeToLook)
        {
            float maxSpeedHorMult = 1f;
            float maxSpeedVerMult = 1f;
            //If speed is larger than maxspeed, change multipliers.
            if (_inputService.HorizontalInput > 0 && velRelativeToLook.x > _playerData.MaxSpeed)
                maxSpeedHorMult = 0;
            if (_inputService.HorizontalInput < 0 && velRelativeToLook.x < -_playerData.MaxSpeed)
                maxSpeedHorMult = 0;
            if (_inputService.VerticalInput > 0 && velRelativeToLook.y > _playerData.MaxSpeed)
                maxSpeedVerMult = 0;
            if (_inputService.VerticalInput < 0 && velRelativeToLook.y < -_playerData.MaxSpeed)
                maxSpeedVerMult = 0;

            return new Vector2(maxSpeedHorMult, maxSpeedVerMult);
        }

        private void Jump()
        {
            if (_player.IsGrounded && _readyToJump)
            {
                _readyToJump = false;

                //Add jump forces
                _rb.AddForce(Vector2.up * (_playerData.JumpForce * 1.5f));
                _rb.AddForce(_player.NormalVector * (_playerData.JumpForce * 0.5f));

                //If jumping while falling, reset y velocity.
                Vector3 vel = _rb.velocity;
                if (_rb.velocity.y < 0.5f)
                    _rb.velocity = new Vector3(vel.x, 0, vel.z);
                else if (_rb.velocity.y > 0)
                    _rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

                StartCoroutine(JumpCooldownRoutine());
            }
        }

        private IEnumerator JumpCooldownRoutine()
        {
            yield return new WaitForSeconds(_playerData.JumpCooldown);
            _readyToJump = true;
        }

        private void CounterMovement(float x, float y, Vector2 mag)
        {
            if (!_player.IsGrounded || _inputService.IsJumping || _player.IsSliding) return;

            //Counter movement
            if (Math.Abs(mag.x) > _playerData.Threshold && Math.Abs(x) < 0.05f ||
                (mag.x < -_playerData.Threshold && x > 0) ||
                (mag.x > _playerData.Threshold && x < 0))
            {
                _rb.AddForce(transform.right *
                             (_playerData.MoveSpeed * Time.deltaTime * -mag.x * _playerData.CounterMovement));
            }

            if (Math.Abs(mag.y) > _playerData.Threshold && Math.Abs(y) < 0.05f ||
                (mag.y < -_playerData.Threshold && y > 0) ||
                (mag.y > _playerData.Threshold && y < 0))
            {
                _rb.AddForce(transform.forward *
                             (_playerData.MoveSpeed * Time.deltaTime * -mag.y * _playerData.CounterMovement));
            }

            //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
            if (Mathf.Sqrt((Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2))) > _playerData.MaxSpeed)
            {
                float fallspeed = _rb.velocity.y;
                Vector3 n = _rb.velocity.normalized * _playerData.MaxSpeed;
                _rb.velocity = new Vector3(n.x, fallspeed, n.z);
            }
        }

        /// <summary>
        /// Find the velocity relative to where the player is looking
        /// Useful for vectors calculations regarding movement and limiting movement
        /// </summary>
        /// <returns></returns>
        public Vector2 FindVelRelativeToLook()
        {
            float lookAngle = transform.eulerAngles.y;
            float moveAngle = Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg;

            float u = Mathf.DeltaAngle(lookAngle, moveAngle);
            float v = 90 - u;

            float magnitue = _rb.velocity.magnitude;
            float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
            float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

            return new Vector2(xMag, yMag);
        }
    }
}