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

        private CreateNewDeckDef createNewDeckDef;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (createNewDeckDef != null)
                DestroyImmediate(createNewDeckDef.newDeck);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            createNewDeckDef = new CreateNewDeckDef();
            tree.Add("New Deck", createNewDeckDef);
            tree.AddAllAssetsAtPath("Decks", "Assets/SO/Decks", typeof(DeckDef_SO), true);
            //tree.AddAllAssetsAtPath("Cards", "Assets/SO/CardDefs", typeof(CardDef_SO), true);

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;

            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                if (SirenixEditorGUI.ToolbarButton("Delete Current"))
                {
                    DeckDef_SO asset = selected.SelectedValue as DeckDef_SO;
                    string path = AssetDatabase.GetAssetPath(asset);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();

        }

        public class CreateNewDeckDef
        {
            [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
            public DeckDef_SO newDeck;
            public CreateNewDeckDef()
            {
                newDeck = ScriptableObject.CreateInstance<DeckDef_SO>();
                newDeck.Name = "New Deck";
            }
            [Button("Create New Deck")]
            public void Create()
            {
                AssetDatabase.CreateAsset(newDeck, "Assets/SO/Decks" + newDeck.Name + ".asset");
                AssetDatabase.SaveAssets();

                //create new SO instance
                newDeck = ScriptableObject.CreateInstance<DeckDef_SO>();
                newDeck.Description = "New Deck";
            }
        }
    }
}
