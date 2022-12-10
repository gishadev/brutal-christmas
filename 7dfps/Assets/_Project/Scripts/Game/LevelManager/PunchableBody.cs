using System;
using Gisha.fpsjam.Game.Core;
using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    [RequireComponent(typeof(Rigidbody))]
    public class PunchableBody : MonoBehaviour, IPunchable
    {
        [SerializeField] private float punchForce = 35f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            _rb.AddForce(punchDir * forceMagnitude, ForceMode.Impulse);
        }
    }
}