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
        [SerializeField]
        private string cardName;
        public string Name => cardName;

        [SerializeField, HideLabel, PreviewField, EnableIf("isEditable")]
        [HorizontalGroup("Top")]
        [VerticalGroup("Top/Left")]
        private Sprite mySprite;
        public Sprite Sprite { get { return mySprite; } }

        [SerializeField, LabelWidth(35), EnableIf("isEditable")]
        [VerticalGroup("Top/Right"), PropertyOrder(1)]
        private GameObject trait;

        [SerializeField, ToggleLeft, HideInTables]
        [VerticalGroup("Top/Right"), PropertyOrder(0)]
        private bool isEditable = true;

        [SerializeField, HideLabel, EnumPaging, EnableIf("isEditable")]
        [VerticalGroup("Bottom")]
        private Suit mySuit = Suit.Spades;
        public Suit Suit { get { return mySuit; } }

        [SerializeField, HideLabel, EnumPaging, EnableIf("isEditable")]
        [VerticalGroup("Bottom")]
        private CardVal myVal = CardVal.Ace;
        public CardVal Val { get { return myVal; } }

        private void OnValidate()
        {
            
        }
    }
}
