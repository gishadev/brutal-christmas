using System;
using UnityEngine;

namespace Gisha.fpsjam.Menu
{
    public class CameraEnter : MonoBehaviour
    {
        public event Action AnimationCompleted;

        public void OnAnim_Completed()
        {
            AnimationCompleted?.Invoke();
        }
    }
}