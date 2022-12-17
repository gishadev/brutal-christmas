using System;

namespace Gisha.fpsjam.Game.InputManager
{
    public interface IInputService
    {
        bool IsWorking { get; }
        float HorizontalInput { get; }
        float VerticalInput { get; }
        bool IsJumping { get; }
        event Action LegPunchButtonDown;
        event Action PickButtonDown;
        event Action<int> NumberButtonDown;
        event Action<float> MouseScroll;
        event Action JumpButtonDown;
        event Action JumpButtonUp;
        event Action LMBDown;
        event Action RMBDown;

        void Init();
        void Dispose();
        void Update();
    }
}