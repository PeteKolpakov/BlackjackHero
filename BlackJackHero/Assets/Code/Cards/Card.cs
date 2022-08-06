using BlackJackHero.Assets.Code.Cards;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJackHero
{
    [RequireComponent(typeof(Image))]
    public class Card : MonoBehaviour
    {
        [Required, SerializeField, InlineEditor, HideInPrefabInstances]
        private CardDef_SO myDef;

        private Image mySprite;

        public CardVal Val { get { return myDef.Val; } }

        public void SetDef(CardDef_SO target)
        {
            myDef = target;
            mySprite = GetComponent<Image>();
        }

        public CardDef_SO GetDef()
        {
            return myDef;
        }




    }
}
