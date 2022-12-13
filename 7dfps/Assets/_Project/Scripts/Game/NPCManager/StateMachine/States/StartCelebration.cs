namespace Gisha.fpsjam.Game.NPCManager
{
    public class StartCelebration : IState
    {
        private readonly INPC _npc;
        private readonly EMOTION_STATE _weakEmotion;
        private readonly EMOTION_STATE _averageEmotion;
        private readonly EMOTION_STATE _maxEmotion;

        public StartCelebration(INPC npc,
            EMOTION_STATE weakEmotion, EMOTION_STATE averageEmotion,
            EMOTION_STATE maxEmotion)
        {
            _npc = npc;
            _weakEmotion = weakEmotion;
            _averageEmotion = averageEmotion;
            _maxEmotion = maxEmotion;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _npc.CelebrationHandler.IsCelebration = false;
            var power = _npc.CelebrationHandler.CelebrationAccumulatedPower;
            if (power >= 0.2f && power < 0.5f)
                _npc.NPCAnimator.SetEmotion(_weakEmotion);
            else if (power >= 0.5f && power < 0.7f)
                _npc.NPCAnimator.SetEmotion(_averageEmotion);
            else if (power >= 0.7f && power < 1f)
                _npc.NPCAnimator.SetEmotion(_maxEmotion);
            else if (power >= 1f)
                _npc.Die();
        }

        public void OnExit()
        {
        }
    }
}