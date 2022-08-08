using UnityEngine;
using UnityEditor.UI;
using TMPro;
using BlackJackHero.Assets.Code.Cards;

namespace BlackJackHero.Assets.Code.Räkning.Managers
{
    internal class DeckDisplayHandler : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI[] modDisplays;
        
        [SerializeField]
        public TextMeshProUGUI[] countDisplays;

        private void Awake()
        {
            foreach (var item in modDisplays)
            {
                item.text = "NO MOD";
            }
        }

        public void SetModDisplay(CardVal val, string target)
        {
            int index = (int)val - 1;
            modDisplays[index].text = target;
        }

        public void SetCountDisplay(CardVal val, string target)
        {
            int index = (int)val - 1;
            modDisplays[index].text = target;
        }
    }
}