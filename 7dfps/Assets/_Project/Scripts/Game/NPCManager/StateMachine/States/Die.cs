using Gisha.Effects.Audio;

namespace Gisha.fpsjam.Game.NPCManager
{
    public class Die : IState
    {
        private INPC _npc;
        private IAudioManager _audioManager;

        public Die(INPC npc, IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _npc = npc;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _audioManager.EmitSpatial("npc_died", _npc.transform.position);
        }

        public void OnExit()
        {
        }
    }
}