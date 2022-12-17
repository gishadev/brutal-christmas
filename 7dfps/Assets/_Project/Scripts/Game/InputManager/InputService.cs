using System;
using Gisha.fpsjam.Infrastructure;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.InputManager
{
    public class InputService : IInputService
    {
        [Inject] private SignalBus _signalBus;

        public bool IsWorking { get; private set; }
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public bool IsJumping { get; private set; }
        public event Action LegPunchButtonDown;
        public event Action PickButtonDown;
        public event Action<int> NumberButtonDown;
        public event Action<float> MouseScroll;
        public event Action JumpButtonDown;
        public event Action JumpButtonUp;
        public event Action LMBDown;
        public event Action RMBDown;

        private KeyCode[] _numberKeyCodes =
            {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};

        public void Init()
        {
            _signalBus.Subscribe<GameStartedSignal>(ResumeInput);
            _signalBus.Subscribe<LoseSignal>(PauseInput);
            _signalBus.Subscribe<WinSignal>(PauseInput);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(ResumeInput);
            _signalBus.Unsubscribe<LoseSignal>(PauseInput);
            _signalBus.Unsubscribe<WinSignal>(PauseInput);
        }

        public void Update()
        {
            if (!IsWorking)
                return;

            HandleKeysInput();
            HandleMouseInput();
        }

        private void PauseInput()
        {
            IsWorking = false;
        }

        private void ResumeInput()
        {
            IsWorking = true;
        }

        private void HandleKeysInput()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
            IsJumping = Input.GetButton("Jump");

            if (Input.GetKeyDown(KeyCode.F))
                LegPunchButtonDown?.Invoke();
            if (Input.GetKeyDown(KeyCode.E))
                PickButtonDown?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space))
                JumpButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.Space))
                JumpButtonUp?.Invoke();

            for (var i = 0; i < _numberKeyCodes.Length; i++)
                if (Input.GetKeyDown(_numberKeyCodes[i]))
                    NumberButtonDown?.Invoke(i);
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                LMBDown?.Invoke();
            if (Input.GetMouseButtonDown(1))
                RMBDown?.Invoke();

            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0f)
                MouseScroll?.Invoke(Input.mouseScrollDelta.y);
        }
    }
}