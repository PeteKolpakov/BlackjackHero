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

        private bool isMatechStarted = false;

        private int burnsAvailable = 0;
        private int holdsAvailable = 0;

        [SerializeField] private DeckDef_SO starterDeck;

        private DisplayHandler dh;

        private void Awake()
        {
            dh = GetComponent<DisplayHandler>();
            p_Deck = new Deck();
            o_Deck = new Deck();
        }
        public void StartGame()
        {
            if (isMatechStarted)
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


            // 2 - Load Decks
            p_Deck.initDeck(starterDeck);
            o_Deck.initDeck(starterDeck);

            // 3 - Play Turn

            DebugState();

            isMatechStarted = true;
        }
        public void EndPlayerTurn()
        {
            // 1 - Deal to player and opponent
            DealToAll();
            DebugState();

            // 2 - player can choose to burn / hold / or replace cards


            // 3 - player completes their turn by pressing the turn button

            // 4 - Opponent Card Mods Activate

            // 5 - Player Card Mods Activate

            // 6 - Sums Are Calculated

            // 7 - Progress Bars Are Updated

            // 8 - Turn Ends
            holdsAvailable = 1;
            burnsAvailable = 2;
            UpdateDisplays();

        }
        private void DebugState()
        {
            print(
                $"Match Initiated: " +
                $"player deck has {p_Deck.CardsCurrent}/{p_Deck.CardsTotal}" +
                $"opponent Deck has {o_Deck.CardsCurrent}/{o_Deck.CardsTotal}" +
                $"p1 = {p1.Val}, p2 = {p2.Val}" +
                $"o1 = {o2.Val}, o2 = {o2.Val}"
                );
        }
        private void DealToAll()
        {
            p1 = p_Deck.PullNextCard();
            p2 = p_Deck.PullNextCard();
            o1 = o_Deck.PullNextCard();
            o2 = o_Deck.PullNextCard();
            UpdateDisplays();
        }
        public void BurnPosition(bool isLeftPos)
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

            UpdateDisplays();
            burnsAvailable--;
        }
        public void HoldPosition(bool isLeftPos)
        {
            if (holdsAvailable <= 0)
            {
                return;
            }

            if (isLeftPos)
            {
                if (ph.Val.Equals(CardVal.NULL))
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
                if (ph.Val.Equals(CardVal.NULL))
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
        private void HitPosition(TargetCardPos target)
        {
            switch (target)
            {
                case TargetCardPos.P1:
                    p1 = p_Deck.PullNextCard();
                    dh.SetCardVal(target, p1.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.P2:
                    p2 = p_Deck.PullNextCard();
                    dh.SetCardVal(target, p2.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.PH:
                    ph = p_Deck.PullNextCard();
                    dh.SetCardVal(target, ph.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O1:
                    o1 = o_Deck.PullNextCard();
                    dh.SetCardVal(target, o1.Val);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O2:
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
                    dh.SetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.P2:
                    p2 = p_Deck.PullNextCard();
                    dh.SetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.PH:
                    ph = p_Deck.PullNextCard();
                    dh.SetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O1:
                    o1 = o_Deck.PullNextCard();
                    dh.SetCardVal(target);
                    UpdateDisplays();
                    break;
                case TargetCardPos.O2:
                    o2 = o_Deck.PullNextCard();
                    dh.SetCardVal(target);
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

            if(!ph.Val.Equals(CardVal.NULL))
                dh.SetCardVal(TargetCardPos.PH, ph.Val);


            dh.SetDisplayText(TargetDisplay.D_PayerSum, GetPlayerSum().ToString());
            dh.SetDisplayText(TargetDisplay.D_BurnCount, burnsAvailable.ToString());
            dh.SetDisplayText(TargetDisplay.D_DeckCount, $"{p_Deck.CardsCurrent}/{p_Deck.CardsTotal}");
            dh.SetDisplayText(TargetDisplay.D_OpponentSum, GetOpponentSum().ToString());
        }
        private int GetPlayerSum()
        {
            int sum = (int)p1.Val + (int)p2.Val;
            return sum;
        }
        private int GetOpponentSum()
        {
            return (int)o1.Val + (int)o2.Val;
        }
    }
}
