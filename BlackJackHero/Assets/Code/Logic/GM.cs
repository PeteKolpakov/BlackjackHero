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
        private int count;

        [SerializeField]
        private List<DeckDef_SO> decks = new List<DeckDef_SO>();

        public TheShoe shoe { get; private set; }

        [SerializeField]
        private GameObject cardPrefab;

        private static GM instance;

        public static GM Instance { get { return instance; } }

        [SerializeField]
        private Player player, dealer;

        // ------ //

        private void Awake()
        {
            InitSingleton();
            shoe = GetComponent<TheShoe>();
            StartGame(4);
        }

        public void UpdateTurn()
        {
            player.FinishTurn();
            dealer.FinishTurn();

            DealToBoth(2);
            dealer.Init();
        }

        public void StartGame(int deckCount)
        {
            FillShoe(deckCount);

            DealToBoth(2);
            dealer.Init();
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
            target.RecieveCard(shoe.PullNextCard(cardPrefab, target.CardHolder));
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
