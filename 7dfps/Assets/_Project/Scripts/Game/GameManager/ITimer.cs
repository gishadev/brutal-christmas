namespace Gisha.fpsjam.Game.GameManager
{
    public interface ITimer
    {
        bool IsTicking { get; }
        float CurrentTime { get; }
        void Start();
        void Pause();
        void Resume();
        void Reset();
        void Tick();
    }
}