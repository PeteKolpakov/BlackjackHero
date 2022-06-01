using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackJackHero
{
    [Serializable]
    public class TheShoe : MonoBehaviour
    {

        [SerializeField]
        private Queue<CardDef_SO> cardsInPlay = new Queue<CardDef_SO>();
        private List<CardDef_SO> burnDeck = new List<CardDef_SO>();

        private Vector2 cardDimentions = new Vector2(1, 2);

        public Card SpawnCard(CardDef_SO def, GameObject prefab, Transform containerObject)
        {
            GameObject spawnedCard = GameObject.Instantiate(prefab, containerObject);
            Card card = spawnedCard.GetComponent<Card>();
            card.SetDef(def);
            card.transform.localScale = cardDimentions;
            //cardsInPlay.Add(card);
            return card;
        }

        public void LoadCard(CardDef_SO target)
        {
            cardsInPlay.Enqueue(target);
        }

        public Card PullNextCard(GameObject prefab, Transform containerObject)
        {
            if (cardsInPlay.Count <= 0)
            {
                Debug.LogWarning("Shoe Empty");
                return null;
            }
            Card spawnedCard = SpawnCard(cardsInPlay.Dequeue(), prefab, containerObject);
            return spawnedCard;
        }

        public void ResetShoe()
        {
            //foreach (var card in cardsInPlay)
            //{
            //    GameObject.Destroy(card.gameObject);
            //}
            cardsInPlay.Clear();
            Debug.Log($"Shoe Clear, Count = {cardsInPlay.Count}");
        }

        public int GetCount()
        {
            return cardsInPlay.Count;
        }

        public void AddToDiscardDeck(CardDef_SO target)
        {
            burnDeck.Add(target);
        }

        public void ShuffleShoe()
        {
            List<CardDef_SO> temp = cardsInPlay.ToList();
            temp = Utils.riffleShuffle(temp, 1);
            cardsInPlay.Clear();
            foreach (var card in temp)
            {
                cardsInPlay.Enqueue(card);
            }
        }
    }
}
