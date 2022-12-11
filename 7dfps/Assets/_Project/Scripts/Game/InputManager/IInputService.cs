using System;

namespace Gisha.fpsjam.Game.InputManager
{
    public interface IInputService
    {
        float HorizontalInput { get; }
        float VerticalInput { get; }
        bool IsJumping { get; }
        event Action LegPunchButtonDown;
        event Action EquipButtonDown;
        event Action<int> NumberButtonDown;
        event Action<float> MouseScroll;
        event Action JumpButtonDown;
        event Action JumpButtonUp;
        event Action LMBDown;
        event Action RMBDown;
        void Update();
    }
}