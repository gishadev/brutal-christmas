using Gisha.Effects.Audio;
using Gisha.fpsjam.Game.Core;
using UnityEngine;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Game.LevelManager
{
    public class Window : MonoBehaviour, IPunchable
    {
        [Inject] private IAudioManager _audioManager;
        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            _audioManager.Play("smash_window", AudioType.SFX);
            gameObject.SetActive(false);
        }
    }
}