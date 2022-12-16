using System;
using Gisha.fpsjam.Game.PlayerGameplay;
using UnityEngine;
using Zenject;

namespace Gisha.fpsjam.Game.Core
{
    public class ObjectsHiderOnDistance : MonoBehaviour
    {
        [SerializeField] private float minSqrDstToShow;
        [SerializeField] private GameObject[] objectsToHide;

        [Inject] private IPlayerManager _playerManager;

        private void LateUpdate()
        {
            var sqrDst = (_playerManager.Player.transform.position - transform.position).sqrMagnitude;
            if (sqrDst < minSqrDstToShow)
                ShowObjects();
            else
                HideObjects();
        }

        private void ShowObjects()
        {
            foreach (var obj in objectsToHide)
                obj.SetActive(true);
        }

        private void HideObjects()
        {
            foreach (var obj in objectsToHide)
                obj.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(minSqrDstToShow));
        }
    }
}