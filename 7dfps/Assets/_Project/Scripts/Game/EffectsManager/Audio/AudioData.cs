using UnityEngine;

namespace Gisha.Effects.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData")]
    public class AudioData : ScriptableObject
    {
        [SerializeField] private Audio[] musicCollection;
        [SerializeField] private Audio[] sfxCollection;

        public Audio[] MusicCollection => musicCollection;
        public Audio[] SfxCollection => sfxCollection;
    }
}