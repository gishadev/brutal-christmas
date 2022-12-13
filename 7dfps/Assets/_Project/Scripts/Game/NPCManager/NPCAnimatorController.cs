using UnityEngine;

namespace Gisha.fpsjam.Game.NPCManager
{
    [RequireComponent(typeof(Animator))]
    public class NPCAnimatorController : MonoBehaviour, INPCAnimatorController
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMovementState(MOVEMENT_STATE movementState)
        {
            _animator.SetInteger("MovementState", (int) movementState);
            _animator.SetTrigger("Movement");
        }

        public void SetEmotion(EMOTION_STATE emotionState)
        {
            _animator.SetInteger("EmotionState", (int) emotionState);
            _animator.SetTrigger("Emotion");
        }
    }

    public enum MOVEMENT_STATE
    {
        IDLE,
        WALK,
        SIT,
        LAY
    }

    public enum EMOTION_STATE
    {
        EXCITED,
        HAPPY,
        TERRIFIED,
        PRAY,
        THINKING
    }
}