using BlackJackHero.Assets.Code.Cards;
using BlackJackHero.Assets.Code.R�kning.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackJackHero
{
    public class CardData 
    {
        private CardVal _myVal;
        public CardVal Val { get => _myVal; }


        private CardMod _myMod;
        public CardMod Mod {get => _myMod; }

        public CardData(CardDef_SO target)
        {
            _myVal = target.Val;
        }        
        
        public CardData()
        {
            _myVal = CardVal.NULL;
        }

        public void SetMod(CardMod target)
        {
            _myMod = target;
        }
    }
}
