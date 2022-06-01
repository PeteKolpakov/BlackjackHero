using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace BlackJackHero
{
    [Serializable]
    public class TheShoe : MonoBehaviour
    {

        [SerializeField]
        private Queue<CardDef_SO> cardsInPlay = new Queue<CardDef_SO>();
        private List<CardDef_SO> burnDeck = new List<CardDef_SO>();

        private Vector2 cardDimentions = new Vector2(1, 2);

        [SerializeField, Required]
        private TextMeshProUGUI shoeFillDisplay;
        private int startSize;

        public Card SpawnCard(CardDef_SO def, GameObject prefab, Transform containerObject)
        {
            GameObject spawnedCard = GameObject.Instantiate(prefab, containerObject);
            Card card = spawnedCard.GetComponent<Card>();
            card.SetDef(def);
            card.transform.localScale = cardDimentions;
            //cardsInPlay.Add(card);
            return card;
        }
        private void UpdateShoeFillDisplay()
        {
            shoeFillDisplay.text = $"Shoe {cardsInPlay.Count}/{startSize}";
        }
        public void ShuffleIfNeeded()
        {
            if (cardsInPlay.Count <= 1)
            {
                if (burnDeck.Count <= 0)
                {
                    Debug.LogWarning("No Cards Left In Burn Deck To Shuffle Into Shoe");
                    return;
                }

                var tempDeck = Utils.riffleShuffle(burnDeck, 4);
                foreach (var card in tempDeck)
                {
                    LoadCard(card);
                }

                burnDeck.Clear();
                Debug.Log("Shoe Shuffled!");
            }
        }

        public void LoadCard(CardDef_SO target)
        {
            cardsInPlay.Enqueue(target);
            UpdateShoeFillDisplay();
        }

        public bool PullNextCard(GameObject prefab, Transform containerObject, out Card pulledCard)
        {
            ShuffleIfNeeded();
            if (cardsInPlay.Count <= 0)
            {
                Debug.LogWarning("Shoe Empty");
                pulledCard = null;
                return false;
            }
            Card spawnedCard = SpawnCard(cardsInPlay.Dequeue(), prefab, containerObject);
            pulledCard = spawnedCard;
            UpdateShoeFillDisplay();
            return true;
        }

        public void ResetShoe()
        {
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
        public void Init()
        {
            startSize = cardsInPlay.Count;
        }
    }
}
