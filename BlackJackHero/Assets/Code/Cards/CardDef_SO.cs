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
        [SerializeField, HideLabel, EnumPaging]
        private CardVal myVal = CardVal.Ace;
        public CardVal Val { get { return myVal; } }
    }
}
