using UnityEngine;

namespace Gisha.fpsjam.Game.UIManager
{
    public static class TimeConverter
    {
        public static string ConvertTime(float time)
        {
            var minutes = Mathf.Floor(time / 60);
            var seconds = (time % 60);
            var fraction = (time * 1000);
            fraction %= 1000;

            return $"{minutes:00}:{seconds:00}:{fraction:000}";
        }
    }
}