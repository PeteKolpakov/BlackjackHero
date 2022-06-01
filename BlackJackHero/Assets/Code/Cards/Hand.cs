using BlackJackHero.Assets.Code.Logic;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BlackJackHero
{
    public class Hand : MonoBehaviour
    {
        [SerializeField]
        List<Card> cards = new List<Card>();
        [SerializeField]
        TextMeshProUGUI handValDisplay;
        [SerializeField]
        private Transform cardHolder;
        private int handValue { get { return GetHandValue(); } }
        public int HandValue { get { return handValue; } }
        public Transform CardHolder { get { return cardHolder; } }
        private HorizontalLayoutGroup horizGroup;
        private Vector3 initPos;

        private float handWidth = 0;

        private void UpdateHandUI()
        {
            float cardWidth = 1f;
            float padding = 0.4f;
            handWidth = GetCardCount() * (cardWidth + padding);
            //cardHolder.transform.position += new Vector3((Screen.width / 2) - (handWidth / 2), 0, 0);
        }

        private void OnValidate()
        {
            cards = new List<Card>();
            initPos = transform.position;
            horizGroup = cardHolder.GetComponent<HorizontalLayoutGroup>();
            UpdateValueDisplay();
        }

        public void RecieveCard(Card target)
        {
            cards.Add(target);
            UpdateValueDisplay();
            UpdateHandUI();
        }

        public void Discard(Card target)
        {
            GM.Instance.shoe.AddToDiscardDeck(target.GetDef());
            Destroy(target.gameObject);
            cards.Remove(target);
        }

        private void UpdateValueDisplay()
        {
            handValDisplay.text = "Hand Value: " + HandValue;
        }

        public void DiscardAll()
        {
            foreach (Card card in cards)
            {
                GM.Instance.shoe.AddToDiscardDeck(card.GetDef());
                Destroy(card.gameObject);
            }
            cards.Clear();
            UpdateHandUI();
        }
        [Button]
        public void PeekHand()
        {
            foreach (var item in cards)
            {
                print($"hand contains - {item.name}");
            }
        }

        public int GetCardCount()
        {
            return cards.Count;
        }
        [Button]
        public int GetHandValue()
        {
            int total = 0;
            foreach (var card in cards)
            {
                total += Utils.GetTrueValueStandard(card.Val, total);
            }
            return total;
        }
    }
}
