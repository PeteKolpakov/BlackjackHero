using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackJackHero.Assets.Code.Räkning.Utils
{
    internal class Deck
    {
        private Queue<CardData> cards   = new Queue<CardData>();
        private List<CardData> burnDeck = new List<CardData>();

        private int cardsTotal;
        public int CardsTotal { get => cardsTotal; }

        private int cardsCurrent;
        public int CardsCurrent { get => cardsCurrent; }

        private int burnDeckCurrent;
        public int BurnDeckCurrent { get => burnDeckCurrent; }

        public void initDeck(DeckDef_SO target)
        {
            cards.Clear();
            burnDeck.Clear();

            CardDef_SO[] targetCards = target.GetCards();
            foreach (var card in targetCards)
            {
                CardData data = new CardData(card);
                cards.Enqueue(data);
            }

            cardsTotal = cards.Count + burnDeck.Count;
            cardsCurrent = cardsTotal;
            burnDeckCurrent = burnDeck.Count;

            Shuffle();
        }

        private void UpdateCardCounts()
        {
            cardsTotal = cards.Count + burnDeck.Count;
            cardsCurrent = cards.Count;
            burnDeckCurrent = burnDeck.Count;
        }

        public void ShuffleIfNeeded()
        {
            if (cards.Count < 1)
            {
                if (burnDeck.Count <= 0)
                {
                    Debug.LogWarning("No Cards Left In Burn Deck To Shuffle Into Shoe");
                    return;
                }
                
                var tempDeck = Utils.riffleShuffle(burnDeck, 6);
                foreach (var card in tempDeck)
                {
                    LoadCard(card);
                }

                burnDeck.Clear();
                Debug.Log("Shoe Shuffled!");
            }
        }

        public void LoadCard(CardData target)
        {
            cards.Enqueue(target);
            UpdateCardCounts();
        }

        public CardData PullNextCard()
        {
            ShuffleIfNeeded();
            if (cards.Count <= 0)
            {
                Debug.LogWarning("Shoe Empty");
                return null;
            }
            CardData spawnedCard = cards.Dequeue();
            UpdateCardCounts();
            return spawnedCard;
        }

        public void AddToDiscardDeck(CardData target)
        {
            burnDeck.Add(target);
        }

        public void Shuffle()
        {
            List<CardData> temp = cards.ToList();

            foreach (var item in burnDeck)
            {
                temp.Add(item);
            }

            temp = Utils.riffleShuffle(temp, 6);
            cards.Clear();
            foreach (var card in temp)
            {
                cards.Enqueue(card);
            }
        }

        public int[] GetCardCountByValue()
        {
            int[] result = new int[13];



            return result;
        }
    }
}
