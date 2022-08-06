using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BlackJackHero.Assets.Code.Logic
{
    public class GM : MonoBehaviour
    {
        [SerializeField]
        private List<DeckDef_SO> decks = new List<DeckDef_SO>();

        public TheShoe shoe { get; private set; }

        [SerializeField]
        private GameObject cardPrefab;

        [SerializeField]
        private UIManager uiManager;

        private static GM instance;

        public static GM Instance { get { return instance; } }

        [SerializeField]
        private Player player, dealer;

        private int currentBet = 0;
        public int CurrentBet { get { return currentBet; }  }

        // ------ //

        //public void PlaceBet()
        //{
        //    if (player.TryPlaceBet())
        //    {
        //        currentBet++;
        //    }
        //    if (!player.TryPlaceBet())
        //    {
        //        // player dies
        //    }
        //}

        private void Awake()
        {
            InitSingleton();
            shoe = GetComponent<TheShoe>();
            StartGame(4);
        }

        public void UpdateTurn()
        {
            // if player hasnt paid in then dealer cant loose lives and instead all hand is discarded
            if (currentBet > 0)
            {
                ResolveBets();

                player.FinishTurn();
                dealer.FinishTurn();
            }
            else
            {
                player.MyHand.DiscardAll();
                dealer.MyHand.DiscardAll();
            }

            currentBet = 0;
            uiManager.UpdateBetDisplay();

            DealToBoth(2);

            player.Init();
            dealer.Init();
        }

        private void ResolveBets()
        {
            // if dealer busts and player wins hand
            if (dealer.CheckIfBust() && !player.CheckIfBust())
            {
                int payout = currentBet * 2;
                player.RecievePayout(payout);
            }
            // if both player and dealer bust
            if (!dealer.CheckIfBust() && !player.CheckIfBust())
            {
                return;
            }
            // if player busts and dealer wins hand
            if (dealer.CheckIfBust() && !player.CheckIfBust())
            {
                int payout = currentBet;
                dealer.RecievePayout(payout);
            }
        }

        public void StartGame(int deckCount)
        {
            FillShoe(deckCount);

            DealToBoth(2);
            dealer.Init();
            player.Init();
        }

        private void DealToBoth(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Hit(player.MyHand);
                Hit(dealer.MyHand);
            }
        }

        public void Hit(Hand target)
        {
            //if (target == player && currentBet == 0)
            //{
            //    if (!player.TryPlaceBet())
            //    {
            //        return;
            //    }
            //}
            Card card; 
            if (shoe.PullNextCard(cardPrefab, target.CardHolder, out card))
            {
                target.RecieveCard(card);
            }
            else
            {
                Debug.Log("No Cards Left To Hit");
            }
        }

        private void FillShoe(int deckCount)
        {
            if (shoe == null)
            {
                return;
            }

            if (decks.Count <= 0)
                return;

            List<CardDef_SO> defsToGen = new List<CardDef_SO>();
            defsToGen.AddRange(decks.SelectMany(deck => deck.GetCards()));
            for (int i = 0; i < deckCount; i++)
            {
                foreach (CardDef_SO def in defsToGen)
                {
                    shoe.LoadCard(def);
                } 
            }
            shoe.Init();
            print("Shoe Filled WIth Cards X" + shoe.GetCount());
        }

        private void ResetShoe()
        {
            if (shoe == null)
            {
                return;
            }

            shoe.ResetShoe();
        }

        [Button]
        private void Shuffle()
        {
            shoe.ShuffleShoe();
        }

        private void InitSingleton()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}
