using System;
using Gisha.fpsjam.Game.Core;
using UnityEngine;

namespace Gisha.fpsjam.Game.LevelManager
{
    [RequireComponent(typeof(Rigidbody))]
    public class PunchableBody : MonoBehaviour, IPunchable
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public void OnPunch(Vector3 punchDir, float forceMagnitude)
        {
            _rb.isKinematic = false;
            _rb.AddForce(punchDir * forceMagnitude, ForceMode.Impulse);
        }
    }
}