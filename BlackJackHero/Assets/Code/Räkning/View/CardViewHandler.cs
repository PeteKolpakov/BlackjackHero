
using UnityEngine;
using TMPro;
using BlackJackHero.Assets.Code.Cards;
using System;

namespace BlackJackHero
{
    public class CardViewHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private GameObject _icon;
        [SerializeField] private GameObject _NullIndicator;

        public void Init()
        {
            _NullIndicator.SetActive(true);
            SetModIconVisible(false);
            SetValDisplay();
        }

        public void SetModIconVisible(bool target)
        {
            switch (target)
            {
                case true:
                    _icon.SetActive(true);
                    break;
                case false:
                    _icon.SetActive(false);
                    break;
            }
        }

        public void SetValDisplay()
        {
            _NullIndicator.SetActive(true);
            string text;
            text = " ";
            _textMesh.text = text;

        }
        public void SetValDisplay(CardVal target)
        {
            _NullIndicator.SetActive(false);
            if (target == CardVal.NULL)
            {
                _textMesh.text = " ";
            }

            string text;
            int i = (int)target;
            text = i.ToString();
            _textMesh.text = text;
        }
    }
}
