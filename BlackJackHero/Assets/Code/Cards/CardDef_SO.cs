using BlackJackHero.Assets.Code.Cards;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackJackHero
{
    [CreateAssetMenu(menuName = "Custom Objects/Card Def")]
    public class CardDef_SO : ScriptableObject
    {

        [SerializeField, HideLabel, EnumToggleButtons]
        [VerticalGroup("Card Definition/Right")]
        private Suit mySuit = Suit.Spades;
        public Suit Suit { get { return mySuit; } }

        [SerializeField, HideLabel, EnumPaging]
        [VerticalGroup("Card Definition/Right")]
        private CardVal myVal = CardVal.Ace;
        public CardVal Val { get { return myVal; } }

        [SerializeField, HideLabel, PreviewField]
        [HorizontalGroup("Card Definition")]
        [VerticalGroup("Card Definition/Left")]
        private Sprite mySprite;
        public Sprite Sprite { get { return mySprite; } }

        private void OnValidate()
        {
            
        }
    }
}
