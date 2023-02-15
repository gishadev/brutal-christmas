﻿using UnityEngine;
using UnityEngine.UI;

namespace Gisha.fpsjam.Game.UIManager
{
    public class SlotUIHandler : MonoBehaviour
    {
        private Image _contentImage;
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _contentImage = transform.GetChild(0).GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void ChangeContent(Sprite sprite)
        {
            _contentImage.gameObject.SetActive(true);
            _contentImage.sprite = sprite;
        }

        public void ClearContent()
        {
            _contentImage.gameObject.SetActive(false);
            _contentImage.sprite = null;
        }
    }
}