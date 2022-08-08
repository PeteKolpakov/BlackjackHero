using BlackJackHero;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    internal class Deck
    {
        private Queue<CardData> cardsInPlay = new Queue<CardData>();

        private int cardsTotal;
        public int CardsTotal { get => cardsTotal; }

        private int cardsCurrent;
        public int CardsCurrent { get => cardsCurrent; }

        private List<CardData> myDeck;

        public void GiveDeck(DeckDef_SO target)
        {
            myDeck = new List<CardData>();
            foreach (var item in target.GetCards())
            {
                CardData card = new CardData(item);
                myDeck.Add(card);
            }
        }

        public void initDeck()
        {
            cardsInPlay.Clear();

            foreach (var data in myDeck)
            {
                cardsInPlay.Enqueue(data);
            }

            cardsTotal = cardsInPlay.Count;
            cardsCurrent = cardsTotal;

            Shuffle();
        }

        private void UpdateCardCounts()
        {
            cardsTotal = cardsInPlay.Count;
            cardsCurrent = cardsInPlay.Count;
        }

        public void LoadCard(CardData target)
        {
            cardsInPlay.Enqueue(target);
            UpdateCardCounts();
        }

        public bool PullNextCard(out CardData pulledCard)
        {

            if (cardsInPlay.Count <= 0)
            {
                Debug.LogWarning("Shoe Empty");
                pulledCard = new CardData();
                return false;
            }
            pulledCard = cardsInPlay.Dequeue();
            UpdateCardCounts();
            return true;
        }

        public void Shuffle()
        {
            List<CardData> temp = cardsInPlay.ToList();

            temp = Utils.riffleShuffle(temp, 6);
            cardsInPlay.Clear();
            foreach (var card in temp)
            {
                cardsInPlay.Enqueue(card);
            }
        }

        public int[] GetCardCountByValue()
        {
            int[] result = new int[13];

            foreach (var item in myDeck)
            {
                int t = (int)item.Val;
                result[t - 1] = t;
            }
            return result;
        }
    }

