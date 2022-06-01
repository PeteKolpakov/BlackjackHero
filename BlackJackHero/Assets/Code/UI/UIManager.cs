using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BlackJackHero.Assets.Code.Logic;

namespace BlackJackHero
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown amountSelectorUI;
        int deckAmount = 4;

        public void OnDeckAmountSelected()
        {
            deckAmount = amountSelectorUI.value + 1;
        }
        public void OnStartButtonPressed()
        {
            GM.Instance.StartGame(deckAmount);
        }
    }
}
