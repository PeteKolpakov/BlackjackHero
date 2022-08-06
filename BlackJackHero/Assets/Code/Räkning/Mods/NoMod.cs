using BlackJackHero.Assets.Code.Räkning.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlackJackHero.Assets.Code.Räkning.Mods
{
    public class NoMod : MonoBehaviour, CardMod
    {
        private string myName = "No Mod";
        public void DeckModAction()
        {

        }

        public string GetName()
        {
            return myName;
        }

        public void TurnModAction()
        {

        }
    }
}
