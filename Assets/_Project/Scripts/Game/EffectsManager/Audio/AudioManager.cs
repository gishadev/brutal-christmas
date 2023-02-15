using System;
using System.Collections.Generic;
using Gisha.Optimisation;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gisha.Effects.Audio
{
    public class AudioManager : PoolManager, IAudioManager
    {
        [Inject] private AudioData _audioData;

        public float MusicVolume { get; private set; } = 1f;
        public float SfxVolume { get; private set; } = 1f;

        private Transform _parent;

        public void InitAmbient()
        {
            _parent = new GameObject("[AUDIO]").transform;
            Object.DontDestroyOnLoad(_parent.gameObject);

            SetupAudio(_audioData.MusicCollection);
            SetupAudio(_audioData.SfxCollection);
        }

        private void SetupAudio(Audio[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                GameObject child = new GameObject(array[i].Name);
                child.transform.SetParent(_parent);

                AudioSource audioSource = child.AddComponent<AudioSource>();

                array[i].GameObject = child;
                array[i].AudioSource = audioSource;

                array[i].AudioSource.clip = array[i].AudioClip;
                array[i].AudioSource.volume = array[i].Volume;
                array[i].AudioSource.pitch = array[i].Pitch;
                array[i].AudioSource.loop = array[i].IsLooping;
            }
        }

        public void Play(string name, AudioType audioType)
        {
            var collection = Array.Empty<Audio>();

            switch (audioType)
            {
                case AudioType.Music:
                    collection = _audioData.MusicCollection;
                    break;
                case AudioType.SFX:
                    collection = _audioData.SfxCollection;
                    break;
            }

            Audio data = Array.Find(collection, x => x.Name == name);
            if (data == null)
            {
                Debug.LogError($"There is no audio with name {name}");
                return;
            }

            data.AudioSource.Play();
        }
        
        public void EmitSpatial(string name, Vector3 position)
        {
            if (!TryEmit(name, PoolObjectType.SFX, out var obj))
                return;

            obj.transform.position = position;
        }
        
        public void SetVolume(float volume, AudioType audioType)
        {
            var collection = new List<Audio>();

            switch (audioType)
            {
                case AudioType.SFX:
                    SfxVolume = volume;
                    collection.AddRange(_audioData.SfxCollection);
                    break;
                case AudioType.Music:
                    MusicVolume = volume;
                    collection.AddRange(_audioData.MusicCollection);
                    break;
            }

            foreach (var audio in collection)
                audio.AudioSource.volume = MusicVolume;
        }
    }

    public enum AudioType
    {
        SFX,
        Music
    }
}