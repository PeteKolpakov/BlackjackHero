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

        public Sprite Sprite { get { return myDef.Sprite; } }

        public Suit Suit { get { return myDef.Suit; } }

        public CardVal Val { get { return myDef.Val; } }

        public void SetDef(CardDef_SO target)
        {
            myDef = target;
            UpdateObjectName();
            mySprite = GetComponent<Image>();
            mySprite.sprite = Sprite;
        }

        public CardDef_SO GetDef()
        {
            return myDef;
        }

        private void OnValidate()
        {
            UpdateObjectName();
        }

        private void UpdateObjectName()
        {
            if (myDef != null) gameObject.name = $"C_{Suit}_{Val}";
            else gameObject.name = "C_Unassigned";
        }
    }
}
