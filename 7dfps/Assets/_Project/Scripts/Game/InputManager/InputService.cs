using System;
using UnityEngine;

namespace Gisha.fpsjam.Game.InputManager
{
    public class InputService : IInputService
    {
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public bool IsJumping { get; private set; }
        public event Action LegPunchButtonDown;
        public event Action JumpButtonDown;
        public event Action JumpButtonUp;
        public event Action LMBButtonDown;
        public event Action RMBButtonDown;

        public void Update()
        {
            HandleKeysInput();
            HandleMouseInput();
        }

        private void HandleKeysInput()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
            IsJumping = Input.GetButton("Jump");

            if (Input.GetKeyDown(KeyCode.F))
                LegPunchButtonDown?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space))
                JumpButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.Space))
                JumpButtonUp?.Invoke();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                LMBButtonDown?.Invoke();
            if (Input.GetMouseButtonDown(1))
                RMBButtonDown?.Invoke();
        }
    }
}