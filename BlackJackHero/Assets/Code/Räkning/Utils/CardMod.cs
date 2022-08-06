using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackHero.Assets.Code.Räkning.Utils
{
    public interface CardMod
    {
        public void TurnModAction();

        public void DeckModAction();

        public String GetName();
    }
}
