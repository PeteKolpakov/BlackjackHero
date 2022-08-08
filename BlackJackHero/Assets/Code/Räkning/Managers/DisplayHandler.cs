using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BlackJackHero.Assets.Code.Cards;
using BlackJackHero.Assets.Code.Räkning;

namespace BlackJackHero
{
    public class DisplayHandler : MonoBehaviour
    {
        [SerializeField] 
        private CardViewHandler 
            C_PlayerCard_1, 
            C_PlayerCard_2, 
            C_PlayerCard_Hold, 
            C_OpponentCard_1, 
            C_OpponentCard_2;

        [SerializeField]
        private TextMeshProUGUI
            D_PayerSum, 
            D_OpponentSum,
            D_BurnCount,
            D_DeckCount,
            D_PlayerScore,
            D_OpponentScore;


        [SerializeField]
        private GameObject
            WinPopup,
            LossPopup;

        [SerializeField]
        private DeckDisplayHandler 
            D_Deck;

        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            InitCards();
            InitText();
        }
        private void InitCards()
        {
            C_PlayerCard_1.Init();
            C_PlayerCard_2.Init();
            C_PlayerCard_Hold.Init();
            C_OpponentCard_1.Init();
            C_OpponentCard_2.Init();
        }
        private void InitText()
        {
            D_PayerSum.text = " ";
            D_OpponentSum.text = " ";
            D_BurnCount.text = " ";
            D_DeckCount.text = " ";
        }
        public void SetDisplayText(TargetDisplay d, string target)
        {
            switch (d)
            {
                case TargetDisplay.D_PayerSum:
                    D_PayerSum.text = target;
                    break;
                case TargetDisplay.D_OpponentSum:
                    D_OpponentSum.text = target;
                    break;
                case TargetDisplay.D_BurnCount:
                    D_BurnCount.text = target;
                    break;
                case TargetDisplay.D_DeckCount:
                    D_DeckCount.text = target;
                    break;
            }
        }
        public void SetCardVal(TargetCardPos d, CardVal target)
        {
            switch (d)
            {
                case TargetCardPos.P1:
                    C_PlayerCard_1.SetValDisplay(target); 
                    break;
                case TargetCardPos.P2:
                    C_PlayerCard_2.SetValDisplay(target);
                    break;
                case TargetCardPos.PH:
                    C_PlayerCard_Hold.SetValDisplay(target);
                    break;
                case TargetCardPos.O1:
                    C_OpponentCard_1.SetValDisplay(target);
                    break;
                case TargetCardPos.O2:
                    C_OpponentCard_2.SetValDisplay(target);
                    break;
            }
        }
        public void ResetCardVal(TargetCardPos d)
        {
            switch (d)
            {
                case TargetCardPos.P1:
                    C_PlayerCard_1.SetValDisplay(); 
                    break;
                case TargetCardPos.P2:
                    C_PlayerCard_2.SetValDisplay();
                    break;
                case TargetCardPos.PH:
                    C_PlayerCard_Hold.SetValDisplay();
                    break;
                case TargetCardPos.O1:
                    C_OpponentCard_1.SetValDisplay();
                    break;
                case TargetCardPos.O2:
                    C_OpponentCard_2.SetValDisplay();
                    break;
            }
        }
        public void ResetScore()
        {
            D_PlayerScore.text = " ";
            D_OpponentScore.text = " ";
        }
        public void SetProgressBarValue(bool isPlayer, int target)
        {
            if (isPlayer)
            {
                D_PlayerScore.text = target.ToString();
            }
            else
            {
                D_OpponentScore.text = target.ToString();
            }
        }
        public void EnablePopup(bool isWin, bool setActive)
        {
            if (isWin)
            {
                WinPopup.SetActive(setActive);
            }
            else
            {
                LossPopup.SetActive(setActive);
            }
        }
        public void SetModDisplay(CardVal targetVal, string target)
        {
            D_Deck.SetModDisplay(targetVal, target);
        }
        public void SetDeckCountDisplay(CardVal targetVal, string target)
        {
            D_Deck.SetCountDisplay(targetVal, target);
        }
        public enum TargetCardPos
        {
            P1,
            P2,
            PH,
            O1,
            O2
        }
        public enum TargetDisplay
        {
            D_PayerSum,
            D_OpponentSum,
            D_BurnCount,
            D_DeckCount
        }
    }
}
