using UnityEngine;

namespace Gisha.Effects.Audio
{
    public interface IAudioManager
    {
        float MusicVolume { get; }
        float SfxVolume { get; }
        void InitAmbient();
        void Init();
        void Play(string name, AudioType audioType);
        void EmitSpatial(string name, Vector3 position);
        void SetVolume(float volume, AudioType audioType);
    }
}