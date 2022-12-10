using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Rotation and Look")] 
        [SerializeField] private float sensitivity = 50f;
        [SerializeField] private float sensMultiplier = 1f;
        
        [Header("Movement")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float moveSpeed = 4500;
        [SerializeField] private float maxSpeed = 20;
        [SerializeField] private float counterMovement = 0.175f;
        [SerializeField] private float threshold = 0.01f;
        [SerializeField] private float maxSlopeAngle = 35f;
        [SerializeField] private float gravityForce = 30f;

        [Header("Jumping")] 
        [SerializeField] private float jumpCooldown = 0.25f;
        [SerializeField] private float jumpForce = 550f;
        
        [Header("Punching")] [SerializeField]
        private float legPunchForce = 35f;

        
        
        public float JumpForce => jumpForce;
        public float MaxSlopeAngle => maxSlopeAngle;
        public float Threshold => threshold;
        public float CounterMovement => counterMovement;
        public LayerMask WhatIsGround => whatIsGround;
        public float MaxSpeed => maxSpeed;
        public float MoveSpeed => moveSpeed;
        public float SensMultiplier => sensMultiplier;
        public float Sensitivity => sensitivity;
        public float JumpCooldown => jumpCooldown;
        public float GravityForce => gravityForce;

        public float LegPunchForce => legPunchForce;
    }
}