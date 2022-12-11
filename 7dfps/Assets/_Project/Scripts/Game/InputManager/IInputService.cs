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
        event Action JumpButtonDown;
        event Action JumpButtonUp;
        event Action LMBButtonDown;
        event Action RMBButtonDown;
        void Update();
    }
}