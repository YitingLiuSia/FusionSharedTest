using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

namespace SiaX
{
#if UNITY_EDITOR
    [CustomEditor(typeof(BrandingManager))]
    [CanEditMultipleObjects]
    public class BrandingManagerEditor : Editor
    {
        SerializedProperty colorPaletteManger;
        SerializedProperty defaultTMPTextObjs;
        SerializedProperty highlightTMPTextObjs;
        SerializedProperty darkerTMPTextObjs;
        SerializedProperty defaultTextObjs;
        SerializedProperty highlightTextObjs;
        SerializedProperty darkerTextObjs;
        SerializedProperty defaultBgImages;
        SerializedProperty highlightBgImages;
        SerializedProperty darkerBgImages;
        SerializedProperty logoObj;
        SerializedProperty defaultMatObjs;
        SerializedProperty highlightMatObjs;
        SerializedProperty darkerMatObjs;
        void OnEnable()
        {
            defaultTMPTextObjs = serializedObject.FindProperty("defaultTMPTextObjs");
            highlightTMPTextObjs = serializedObject.FindProperty("highlightTMPTextObjs");
            darkerTMPTextObjs = serializedObject.FindProperty("darkerTMPTextObjs");

            defaultTextObjs = serializedObject.FindProperty("defaultTextObjs");
            highlightTextObjs = serializedObject.FindProperty("highlightTextObjs");
            darkerTextObjs = serializedObject.FindProperty("darkerTextObjs");

            defaultBgImages = serializedObject.FindProperty("defaultBgImages");
            highlightBgImages = serializedObject.FindProperty("highlightBgImages");
            darkerBgImages = serializedObject.FindProperty("darkerBgImages");
            
            logoObj = serializedObject.FindProperty("logoObj");

            defaultMatObjs = serializedObject.FindProperty("defaultMatObjs");
            highlightMatObjs = serializedObject.FindProperty("highlightMatObjs");
            darkerMatObjs = serializedObject.FindProperty("darkerMatObjs");

            colorPaletteManger = serializedObject.FindProperty("colorPaletteManger");
        }
        // when the enum is selected, display the selected content in the editor 
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BrandingManager bm = (BrandingManager)target;
            EditorGUILayout.PropertyField(colorPaletteManger, new GUIContent("Color Palette Manager"), GUILayout.ExpandHeight(true));
            bm.selections = (BrandingManager.Selections)EditorGUILayout.EnumPopup("selections", bm.selections);

            switch (bm.selections)
            {
                case BrandingManager.Selections.UI:
                    UISelection();

                    break;
                case BrandingManager.Selections.objects:
                    ObjectSelection();
                    break;

                case BrandingManager.Selections.both:

                    ObjectSelection();
                    UISelection();

                    break;

                }
            // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
        private void ObjectSelection()
        {
            EditorGUILayout.PropertyField(defaultMatObjs, new GUIContent("Default Mat Objs"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(highlightMatObjs, new GUIContent("Highlight Mat Objs"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(darkerMatObjs, new GUIContent("Dark Mat Objs"), GUILayout.ExpandHeight(true));
        }
        private void UISelection()
        {
            EditorGUILayout.PropertyField(defaultBgImages, new GUIContent("Default Bg Images"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(highlightBgImages, new GUIContent("Highlight Bg Images"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(darkerBgImages, new GUIContent("Darker Bg Images"), GUILayout.ExpandHeight(true));

            EditorGUILayout.PropertyField(defaultTMPTextObjs, new GUIContent("Default TMPro Texts"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(highlightTMPTextObjs, new GUIContent("Highlight TMPro Texts"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(darkerTMPTextObjs, new GUIContent("Darker TMPro Texts"), GUILayout.ExpandHeight(true));

            EditorGUILayout.PropertyField(defaultTextObjs, new GUIContent("Default Texts"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(highlightTextObjs, new GUIContent("Highlight Texts"), GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(darkerTextObjs, new GUIContent("Darker Texts"), GUILayout.ExpandHeight(true));
            
            EditorGUILayout.PropertyField(logoObj, new GUIContent("Logo"), GUILayout.ExpandHeight(true));
        }
    }

#endif
    public class BrandingManager : MonoBehaviour
    {
        public ColorPaletteManager colorPaletteManger;
        public enum Selections { UI, objects, Visualization, both };
        public Selections selections = Selections.UI;
        public Image logoObj;

        public List<MeshRenderer> defaultMatObjs;
        public List<MeshRenderer> highlightMatObjs;
        public List<MeshRenderer> darkerMatObjs;

        public List<TextMeshProUGUI> defaultTMPTextObjs;
        public List<TextMeshProUGUI> highlightTMPTextObjs;
        public List<TextMeshProUGUI> darkerTMPTextObjs;

        public List<Text> defaultTextObjs;
        public List<Text> highlightTextObjs;
        public List<Text> darkerTextObjs;

        public List<Image> defaultBgImages;
        public List<Image> highlightBgImages;
        public List<Image> darkerBgImages;
        void Start()
        {
            Init();
        }
        private void OnValidate()
        {
            Init();
        }
        void Init()
        {
            switch (selections)
            {
                case Selections.UI:
                    InitUI();
                    break;
                case Selections.objects:
                    InitRenderer();
                    break;

                case Selections.both:
                    InitRenderer();
                    InitUI();
                    break;
            }
        }
        void SetMaterial(List<MeshRenderer> renders, Material aMat)
        {
            foreach (var d in renders)
            {
                if (d != null)
                    d.material = aMat;
            }
        }
        private void InitRenderer()
        {
            // Mesh Rednerers 
            SetMaterial(defaultMatObjs, colorPaletteManger.defaultMaterial);
            SetMaterial(highlightMatObjs, colorPaletteManger.highlightMaterial);
            SetMaterial(darkerMatObjs, colorPaletteManger.darkerMaterial);
        }
        private void InitUI()
        {
            // UI text 
            SetColor(defaultTextObjs, colorPaletteManger.defaultColor);
            SetColor(highlightTextObjs, colorPaletteManger.highlightColor);
            SetColor(darkerTextObjs, colorPaletteManger.darkerColor);

            // UI TMPro text 
            SetColor(defaultTMPTextObjs, colorPaletteManger.defaultColor);
            SetColor(highlightTMPTextObjs, colorPaletteManger.highlightColor);
            SetColor(darkerTMPTextObjs, colorPaletteManger.darkerColor);

            // UI Background 
            SetColor(defaultBgImages, colorPaletteManger.defaultColor);
            SetColor(highlightBgImages, colorPaletteManger.highlightColor);
            SetColor(darkerBgImages, colorPaletteManger.darkerColor);

            if (logoObj != null)
                logoObj.sprite = colorPaletteManger.logo;
        }

        private void SetColor(List<Image> images, Color aColor)
        {
            foreach (var a in images)
            {
                if (a != null)
                    a.color = aColor;
            }
        }
        private void SetColor(List<Text> images, Color aColor)
        {
            foreach (var a in images)
            {
                if (a != null)
                    a.color = aColor;
            }
        }

        private void SetColor(List<TextMeshProUGUI> texts, Color aColor)
        {
            foreach (var a in texts)
            {
                if (a != null)
                    a.color = aColor;
            }
        }
    }
}

