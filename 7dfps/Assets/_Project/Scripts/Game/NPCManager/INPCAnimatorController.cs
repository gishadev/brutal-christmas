namespace Gisha.fpsjam.Game.NPCManager
{
    public interface INPCAnimatorController
    {
        void SetMovementState(MOVEMENT_STATE movementState);
        void SetEmotion(EMOTION_STATE emotionState);
        float GetCurrentAnimationLength();
    }
}