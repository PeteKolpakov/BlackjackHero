using BlackJackHero.Assets.Code.Logic;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackJackHero
{
    public class Dealer : Player
    {
        public void DealerHitCheck()
        {
            while (Value <= 17)
            {
                GM.Instance.Hit(MyHand);
                Debug.Log("Dealer Hits");
            }
        }

        public override void Init()
        {
            DealerHitCheck();
            base.Init();

        }
    }
}
