using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackJackHero
{
    [CreateAssetMenu(menuName = "Custom Objects/Deck Definition")]
    public class DeckDef_SO : ScriptableObject
    {
        public string Name;

        [TextArea]
        public String Description;

        [SerializeField, TableList, ListDrawerSettings(NumberOfItemsPerPage = 8)]
        private List<CardDef_SO> cards = new List<CardDef_SO>();

        public int Size { get { return cards.Count; } }

        public CardDef_SO[] GetCards()
        {
            List<CardDef_SO> shuffledDeck = Utils.riffleShuffle(cards, 1);
            return shuffledDeck.ToArray();
        }
    }
}
