using System;
using System.Collections;
using Gisha.fpsjam.Game.InputManager;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    [RequireComponent(typeof(PlayerMovement), typeof(Player))]
    public class Sliding : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerData _playerData;

        public event Action SlidingStarted;
        public event Action SlidingEnded;

        private bool _readyToSlide = true;

        private Vector3 _playerScale;
        private Rigidbody _rb;
        private Player _player;

        [Inject]
        private void Construct(IInputService inputService, PlayerData playerData)
        {
            _inputService = inputService;
            _playerData = playerData;
        }

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _playerScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (_player.IsSliding)
                CounterSlide();
        }

        private void OnEnable() => _inputService.CrouchButtonDown += OnCrouchButtonDown;
        private void OnDisable() => _inputService.CrouchButtonDown -= OnCrouchButtonDown;

        private void OnCrouchButtonDown()
        {
            if (_rb.velocity.magnitude > 0.5f && _readyToSlide && !_player.IsSliding)
            {
                if (_player.IsGrounded)
                    Slide();
                else
                    StartCoroutine(CoyoteSlideRoutine());
            }
        }

        private void CounterSlide()
        {
            _rb.AddForce(-_rb.velocity.normalized *
                         (_playerData.MoveSpeed * Time.deltaTime * _playerData.SlideCounterMovement));
        }


        private void Slide()
        {
            transform.localScale = _playerData.CrouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f,
                transform.position.z);
            _rb.AddForce(transform.forward * _playerData.SlideForce);
            StartCoroutine(ResetSlideRoutine());

            SlidingStarted?.Invoke();
        }

        private IEnumerator CoyoteSlideRoutine()
        {
            float timeElapsed = 0f;
            while (timeElapsed < _playerData.CoyoteSlideTimeInSeconds)
            {
                if (_player.IsGrounded)
                {
                    Slide();
                    break;
                }

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator ResetSlideRoutine()
        {
            yield return new WaitForSeconds(_playerData.SlideTimeInSeconds);
            transform.localScale = _playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            StartCoroutine(SlideCooldownRoutine());

            SlidingEnded?.Invoke();
        }

        private IEnumerator SlideCooldownRoutine()
        {
            _readyToSlide = false;
            yield return new WaitForSeconds(_playerData.SlideCooldown);
            _readyToSlide = true;
        }
    }
}