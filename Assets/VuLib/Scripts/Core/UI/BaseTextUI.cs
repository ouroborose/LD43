using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VuLib
{
    public class BaseTextUI : MonoBehaviour, IShowHide
    {
        public TextMeshPro _text;

        public void Hide(bool instant = false)
        {
            gameObject.SetActive(false);
        }

        public void Show(bool instant = false)
        {
            gameObject.SetActive(true);
        }

        protected void Awake()
        {
            if(_text == null)
            {
                _text = GetComponentInChildren<TextMeshPro>();
            }
        }
    }
}

