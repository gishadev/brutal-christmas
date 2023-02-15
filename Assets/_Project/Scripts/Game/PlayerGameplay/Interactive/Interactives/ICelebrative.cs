namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public interface ICelebrative
    {
        float EmittingCelebrationPower { get; }
        void EmitCelebration(float power);
    }
}