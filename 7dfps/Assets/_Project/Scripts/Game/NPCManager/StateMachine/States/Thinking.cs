namespace Gisha.fpsjam.Game.NPCManager
{
    public class Thinking : IState
    {
        private INPCAnimatorController _npcAnimator;

        public Thinking(INPCAnimatorController npcAnimator)
        {
            _npcAnimator = npcAnimator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _npcAnimator.SetEmotion(EMOTION_STATE.THINKING);
        }

        public void OnExit()
        {
        }
    }
}