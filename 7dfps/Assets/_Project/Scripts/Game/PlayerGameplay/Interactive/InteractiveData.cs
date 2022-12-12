﻿using UnityEngine;

namespace Gisha.fpsjam.Game.PlayerGameplay.Interactive
{
    [CreateAssetMenu(fileName = "InteractiveData", menuName = "ScriptableObjects/InteractiveData")]
    public class InteractiveData : ScriptableObject
    {
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private GameObject prefab;

        public Sprite IconSprite => iconSprite;

        public GameObject Prefab => prefab;
    }
}