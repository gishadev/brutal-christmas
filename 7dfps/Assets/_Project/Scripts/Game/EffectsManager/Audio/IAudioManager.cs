namespace Gisha.Effects.Audio
{
    public interface IAudioManager
    {
        float MusicVolume { get; }
        float SfxVolume { get; }
        void Init();
        void Play(string name, AudioType audioType);
        void SetVolume(float volume, AudioType audioType);
    }
}