using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Skins))]
public class SkinsEditor : Editor
{
    private bool isFoldout;
    private ReorderableList list;
    private GUIContent healthLabel;
    private int indent = 10;
    private const int lengthSideSquare = 80;
    private Texture skinBodyTexture;
    private Texture skinHandTexture;
    private int currentCountSkins;
    private Skins skins;
    private void OnEnable()
    {
        healthLabel = new GUIContent();
        healthLabel.text = "Health";
        skins = ((Skins)target);
        list = new ReorderableList(Skins.skins,typeof(Skins),true,true,true,true);
        list.drawElementCallback = DrawElementCallback;
        list.elementHeightCallback = (index) => 
        {
            return EditorGUIUtility.singleLineHeight * 8 + indent * 4;
        };
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect,"Skins");
        };
        list.onAddCallback = list =>
        {
            MainUIHandler mainUIHandler = FindObjectOfType<MainUIHandler>();
            mainUIHandler.UIAddSkinsToList(list.list,Skins.prefabSkinCell);
        };
        list.onRemoveCallback = list => 
        {
            MainUIHandler mainUIHandler = FindObjectOfType<MainUIHandler>();
            mainUIHandler.UIRemoveSkinToList(list);
        };
    }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        Skin element = Skins.skins[index];
            if(isActive)
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"ATK");
            element.attackDamage = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.attackDamage);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            element.health = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.health);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"STM");
            element.stamina = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.stamina);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"CRI");
            element.criticalDamage = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.criticalDamage);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"AGI");
            element.attackSpeed = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.attackSpeed);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"DEF");
            element.protection = EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.protection);

            //Sprites of the character's body and arms
                                    
            element.spriteSkinBody = (Sprite) EditorGUI.ObjectField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                (Object) element.spriteSkinBody, typeof(Sprite),true);

            element.spriteSkinHand = (Sprite) EditorGUI.ObjectField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + indent,rect.width / 6 * 3,18),
                (Object) element.spriteSkinHand, typeof(Sprite),true);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,39,18),"Price");
                element.price = EditorGUI.IntField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,rect.width / 2,18),element.price);
            }
            else
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"ATK");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.attackDamage);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.health);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"STM");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.stamina);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"CRI");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.criticalDamage);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"AGI");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.attackSpeed);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"DEF");
            EditorGUI.FloatField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.protection);

            //Sprites of the character's body and arms
                                    
            EditorGUI.ObjectField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                (Object) element.spriteSkinBody, typeof(Sprite),true);

            EditorGUI.ObjectField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + indent,rect.width / 6 * 3,18),
                (Object) element.spriteSkinHand, typeof(Sprite),true);

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,39,18),"Price");
                EditorGUI.IntField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,rect.width / 2,18),element.price);
            }
            skinBodyTexture = skinHandTexture = null;
            if(element?.spriteSkinBody)
            {
                skinBodyTexture = element.spriteSkinBody.texture;
            }
            if(element?.spriteSkinHand)
            {
                skinHandTexture = element.spriteSkinHand.texture;
            }
                Rect upperRect = new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,lengthSideSquare,lengthSideSquare);
                Rect lowerRect = new Rect(rect.x + lengthSideSquare,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent * 3,lengthSideSquare / 2,lengthSideSquare / 2);
                if(skinBodyTexture != null)
                {
                    GUI.DrawTexture(upperRect,skinBodyTexture);
                }
                if(skinHandTexture != null)
                {
                    GUI.DrawTexture(lowerRect, skinHandTexture);
                }
    }
    public override void OnInspectorGUI()
    {
        Skins.prefabSkinCell = (GameObject) EditorGUILayout.ObjectField("Skin cell",(Object) Skins.prefabSkinCell,typeof(GameObject),false);
        if(currentCountSkins != Skins.skins.Count)
        {
            currentCountSkins = Skins.skins.Count;
            SetObjectDitry(skins.gameObject);
        }
        list.DoLayoutList();
        
    }
    public static void SetObjectDitry(GameObject obj) {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }
}
