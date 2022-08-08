using BlackJackHero.Assets.Code.Räkning.Utils;


namespace BlackJackHero.Assets.Code.Räkning.Mods
{
   
    public class NoMod : CardMod
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
