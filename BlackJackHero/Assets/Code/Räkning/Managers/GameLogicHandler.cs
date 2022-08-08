using BlackJackHero.Assets.Code.Cards;
using BlackJackHero.Assets.Code.Räkning.Utils;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BlackJackHero.DisplayHandler;

namespace BlackJackHero
{
    [RequireComponent(typeof(DisplayHandler))]
    public class GameLogicHandler : MonoBehaviour
    {       
        private CardData
            p1,
            p2,
            ph,
            o1,
            o2;

        private Deck
            p_Deck,
            o_Deck;        

        private bool isMatchRunning = false;

        private int burnsAvailable = 0;
        private int holdsAvailable = 0;
        private int holdHitsAvailable = 0;

        private int matchScoreGoal = 100;
        private int playerScore = 0;
        private int opponentScore = 0;

        [SerializeField] private DeckDef_SO starterDeck;

        private DisplayHandler dh;

        private void Awake()
        {
            dh = GetComponent<DisplayHandler>();
            p_Deck = new Deck();
            o_Deck = new Deck();

            dh.ResetProgressBarValue();
        }
        public void  StartGame()
        {
            if (isMatchRunning)
            {
                print("Match Has Already Begun");
                return;
            }
            // 1 - Clear/Init everything
            dh.Init();

            p1 = new CardData();
            p2 = new CardData();
            ph = new CardData();
            o1 = new CardData();
            o2 = new CardData();

            playerScore = 0;
            opponentScore = 0;

            dh.ResetProgressBarValue();

            // 2 - Load Decks
            p_Deck.initDeck(starterDeck);
            o_Deck.initDeck(starterDeck);

            // 3 - Play Turn

            DebugState();

            isMatchRunning = true;

            // Deal to player and opponent
            DealToAll();
            DebugState();

            // Turn Ends
            holdsAvailable = 1;
            holdHitsAvailable = 1;
            burnsAvailable = 2;
            UpdateDisplays();
        }
        public void  EndPlayerTurn()
        {
            // player completes their turn by pressing the turn button

            // Opponent Card Mods Activate

            // Player Card Mods Activate

            // Sums Are Calculated
            playerScore   += GetPlayerSum();
            opponentScore += GetOpponentSum();

            // Progress Bars Are Updated
            dh.SetProgressBarValue(true,  playerScore);
            dh.SetProgressBarValue(false, opponentScore);

            // Check For Win
            if (CheckForWin(opponentScore))  // check opponent first?
            {
                print("Opponent Wins!!");
                HandleWin(false);
            }
            if (CheckForWin(playerScore))
            {
                print("Player Wins!!");
                HandleWin(true);
            }

            // Deal to player and opponent
            DealToAll();
            DebugState();

            // Turn Ends
            holdsAvailable = 1;
            holdHitsAvailable = 1;
            burnsAvailable = 2;
            UpdateDisplays();

            // player can choose to burn / hold / or replace cards
        }
        private void DebugState()
        {
            print(
                $"Match Initiated: player deck has {p_Deck.CardsCurrent}/{p_Deck.CardsTotal} and {p_Deck.BurnDeckCurrent} burnt" +
                                 $"opponent Deck has {o_Deck.CardsCurrent}/{o_Deck.CardsTotal} and {o_Deck.BurnDeckCurrent} burnt" +
                $"p1 = {p1.Val}, p2 = {p2.Val}, ph = {ph.Val} " +
                $"o1 = {o2.Val}, o2 = {o2.Val}"
                );
        }
        private void DealToAll()
        {

            DiscardPosition(TargetCardPos.P1);
            p1 = p_Deck.PullNextCard();
            DiscardPosition(TargetCardPos.P2);
            p2 = p_Deck.PullNextCard();
            DiscardPosition(TargetCardPos.O1);
            o1 = o_Deck.PullNextCard();
            DiscardPosition(TargetCardPos.O2);
            o2 = o_Deck.PullNextCard();
            UpdateDisplays();
        }
        public void  BurnPosition(bool isLeftPos)
        {
            if (burnsAvailable <= 0)
            {
                return;
            }

            if (isLeftPos)
            {
                DiscardPosition(TargetCardPos.P1);
                HitPosition(TargetCardPos.P1);
                dh.SetCardVal(TargetCardPos.P1, p1.Val);
            }
            else
            {
                DiscardPosition(TargetCardPos.P2);
                HitPosition(TargetCardPos.P2);
                dh.SetCardVal(TargetCardPos.P1, p2.Val);
            }

            burnsAvailable--;
            UpdateDisplays();
        }
        public void  HoldPosition(bool isLeftPos)
        {
            if (holdsAvailable <= 0)
            {
                return;
            }

            if (isLeftPos)
            {
                if (!ph.Val.Equals(CardVal.NULL))
                {
                    DiscardPosition(TargetCardPos.PH);
                }

                ph = p1;
                dh.SetCardVal(TargetCardPos.PH, ph.Val);
                
                DiscardPosition(TargetCardPos.P1);
                HitPosition(TargetCardPos.P1);
                dh.SetCardVal(TargetCardPos.P1, p1.Val);
            }
            else
            {
                if (!ph.Val.Equals(CardVal.NULL))
                {
                    DiscardPosition(TargetCardPos.PH);
                }

                ph = p2;
                dh.SetCardVal(TargetCardPos.PH, ph.Val);

                DiscardPosition(TargetCardPos.P2);
                HitPosition(TargetCardPos.P2);
                dh.SetCardVal(TargetCardPos.P2, p2.Val);
            }
            UpdateDisplays();
            holdsAvailable--;
        }
        public void  PlayHoldCard(bool isleftButton)
        {
            if (holdHitsAvailable <= 0)
            {
                return ;
            }

            if (isleftButton)
            {
                if (!p1.Val.Equals(CardVal.NULL))
                {
                    DiscardPosition(TargetCardPos.P1);
                }

                p1 = ph;
                dh.SetCardVal(TargetCardPos.P2, p1.Val);

                DiscardPosition(TargetCardPos.PH);
                dh.SetCardVal(TargetCardPos.PH, CardVal.NULL);
            }
            else
            {
                if (!p2.Val.Equals(CardVal.NULL))
                {
                    DiscardPosition(TargetCardPos.P2);
                }

                p2 = ph;
                dh.SetCardVal(TargetCardPos.P2, p2.Val);

                DiscardPosition(TargetCardPos.PH);
                dh.SetCardVal(TargetCardPos.PH, CardVal.NULL);
            }

            holdHitsAvailable--;
            UpdateDisplays();
        }
        private void HitPosition(TargetCardPos target)
        {
            switch (target)
            {
                case TargetCardPos.P1:
                    p_Deck.AddToDiscardDeck(p1);
                    p1 = p_Deck.PullNextCard();
                    dh.SetCardVal(target, p1.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.P2:
                    p_Deck.AddToDiscardDeck(p2);
                    p2 = p_Deck.PullNextCard();
                    dh.SetCardVal(target, p2.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.PH:
                    p_Deck.AddToDiscardDeck(ph);
                    ph = p_Deck.PullNextCard();
                    dh.SetCardVal(target, ph.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O1:
                    p_Deck.AddToDiscardDeck(o1);
                    o1 = o_Deck.PullNextCard();
                    dh.SetCardVal(target, o1.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O2:
                    p_Deck.AddToDiscardDeck(o2);
                    o2 = o_Deck.PullNextCard();
                    dh.SetCardVal(target, o2.Val);
                    UpdateDisplays();
                    break;
            }
        }
        private void DiscardPosition(TargetCardPos target)
        {
            switch (target)
            {
                case TargetCardPos.P1:
                    p_Deck.AddToDiscardDeck(p1);
                    dh.ResetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.P2:
                    p_Deck.AddToDiscardDeck(p2);
                    p2 = p_Deck.PullNextCard();
                    dh.ResetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.PH:
                    p_Deck.AddToDiscardDeck(ph);
                    dh.ResetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O1:
                    p_Deck.AddToDiscardDeck(o1);
                    o1 = o_Deck.PullNextCard();
                    dh.ResetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O2:
                    p_Deck.AddToDiscardDeck(o2);
                    o2 = o_Deck.PullNextCard();
                    dh.ResetCardVal(target);
                    UpdateDisplays();
                    break;
            }
        }
        private void UpdateDisplays()
        {
            dh.SetCardVal(TargetCardPos.P1, p1.Val);
            dh.SetCardVal(TargetCardPos.P2, p2.Val);
            dh.SetCardVal(TargetCardPos.O1, o1.Val);
            dh.SetCardVal(TargetCardPos.O2, o2.Val);

            dh.SetCardVal(TargetCardPos.PH, ph.Val);

            dh.SetDisplayText(TargetDisplay.D_PayerSum, GetPlayerSum().ToString());
            dh.SetDisplayText(TargetDisplay.D_BurnCount, burnsAvailable.ToString());
            dh.SetDisplayText(TargetDisplay.D_DeckCount, $"{p_Deck.CardsCurrent}/{p_Deck.CardsCurrent + p_Deck.BurnDeckCurrent}");
            dh.SetDisplayText(TargetDisplay.D_OpponentSum, GetOpponentSum().ToString());

            dh.SetProgressBarValue(true,  playerScore);
            dh.SetProgressBarValue(false, opponentScore);
        }
        private int  GetPlayerSum()
        {
            int sum = (int)p1.Val + (int)p2.Val;
            return sum;
        }
        private int  GetOpponentSum()
        {
            return (int)o1.Val + (int)o2.Val;
        }
        private bool CheckForWin(int score)
        {
            if (score >= matchScoreGoal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void HandleWin(bool isPlayerWin)
        {
            dh.EnablePopup(isPlayerWin, true);
            isMatchRunning = false;
        }
        
    }
}
