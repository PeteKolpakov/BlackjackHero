using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace BlackJackHero
{
    public class CardData_Editor : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Deck Editor")]
        private static void OpenWindow()
        {
            GetWindow<CardData_Editor>().Show();
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            tree.Add("New Deck", new CreateNewDeckDef());
            tree.AddAllAssetsAtPath("Decks", "Assets/SO/Decks", typeof(DeckDef_SO), true);
            tree.AddAllAssetsAtPath("Cards", "Assets/SO/CardDefs", typeof(CardDef_SO), true);

            return tree;
        }

        public class CreateNewDeckDef
        {
            [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
            public DeckDef_SO newDeck;
            public CreateNewDeckDef()
            {
                newDeck = ScriptableObject.CreateInstance<DeckDef_SO>();
                newDeck.Description = "New Deck";
            }
            [Button("Create New Deck")]
            public void Create()
            {
                AssetDatabase.CreateAsset(newDeck, "Assets/SO/Decks" + newDeck.Description + ".asset");
                AssetDatabase.SaveAssets();

                newDeck = ScriptableObject.CreateInstance<DeckDef_SO>();
                newDeck.Description = "New Deck";
            }
        }
    }
}
