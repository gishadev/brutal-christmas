using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    public class Player : MonoBehaviour, IPlayer
    {
        [Inject] private PlayerData _playerData;

        public Vector3 NormalVector => _normalVector;
        public bool IsGrounded => _isGrounded;
        public bool IsSliding => _isSliding;

        private bool _isGrounded;
        private bool _isSliding;
        private Vector3 _normalVector = Vector3.up;
        private bool _cancellingGrounded;
        
        #region Ground Detection

        private bool IsFloor(Vector3 v)
        {
            float angle = Vector3.Angle(Vector3.up, v);
            return angle < _playerData.MaxSlopeAngle;
        }

        /// <summary>
        /// Handle ground detection
        /// </summary>
        private void OnCollisionStay(Collision other)
        {
            //Make sure we are only checking for walkable layers
            int layer = other.gameObject.layer;
            if (_playerData.WhatIsGround != (_playerData.WhatIsGround | (1 << layer))) return;

            //Iterate through every collision in a physics update
            for (int i = 0; i < other.contactCount; i++)
            {
                Vector3 normal = other.contacts[i].normal;
                if (IsFloor(normal))
                {
                    _isGrounded = true;
                    _cancellingGrounded = false;
                    _normalVector = normal;
                    CancelInvoke(nameof(StopGrounded));
                }
            }

            //Invoke ground/wall cancel, since we can't check normals with CollisionExit
            float delay = 3f;
            if (!_cancellingGrounded)
            {
                _cancellingGrounded = true;
                Invoke(nameof(StopGrounded), Time.deltaTime * delay);
            }
        }

        private void StopGrounded()
        {
            _isGrounded = false;
        }

        #endregion

    }
}