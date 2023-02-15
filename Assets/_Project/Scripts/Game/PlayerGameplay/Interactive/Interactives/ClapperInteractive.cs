﻿using Gisha.Effects.Audio;
using Gisha.Effects.VFX;
using Gisha.fpsjam.Game.NPCManager;
using UnityEngine;
using Zenject;
using AudioType = Gisha.Effects.Audio.AudioType;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    public class ClapperInteractive : Interactive, ICelebrative
    {
        [SerializeField] private Transform shotPoint;
        [SerializeField] private float raycastDst = 2f;
        [SerializeField] private float raycastRadius = 2f;
        [Space] [SerializeField] private float emittingCelebrationPower = 0.25f;
        [SerializeField] private string sfxName = "clapper_small";
        
        public float EmittingCelebrationPower => emittingCelebrationPower;

        [Inject] private IVFXManager _vfxManager;
        [Inject] private IAudioManager _audioManager;

        public override void Use()
        {
            Debug.Log("Boom!");

            EmitCelebration(0.25f);
            _vfxManager.EmitAt("clapper_explosion", shotPoint.transform.position, shotPoint.transform.rotation);
            _audioManager.Play(sfxName, AudioType.SFX);
        }

        public void EmitCelebration(float power)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var hits = Physics.SphereCastAll(ray, raycastRadius, raycastDst);

            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider == null)
                    continue;

                if (!hitInfo.collider.TryGetComponent(out INPC npc))
                    continue;

                npc.CelebrationHandler.Celebrate(EmittingCelebrationPower);
            }
        }
    }
}