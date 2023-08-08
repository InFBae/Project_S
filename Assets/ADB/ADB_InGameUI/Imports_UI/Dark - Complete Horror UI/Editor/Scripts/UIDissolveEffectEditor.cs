using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(UIDissolveEffect))]
    public class UIDissolveEffectEditor : Editor
    {
        private UIDissolveEffect uideTarget;
        private int currentTab;

        private void OnEnable()
        {
            uideTarget = (UIDissolveEffect)target;
        }

        public override void OnInspectorGUI()
        {
            GUISkin customSkin;
            Color defaultColor = GUI.color;

            if (EditorGUIUtility.isProSkin == true)
                customSkin = (GUISkin)Resources.Load("Editor\\DUI Skin Dark");
            else
                customSkin = (GUISkin)Resources.Load("Editor\\DUI Skin Light");

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = defaultColor;

            // Top Header
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("DE Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

            // Toolbar content
            GUIContent[] toolbarTabs = new GUIContent[1];
            toolbarTabs[0] = new GUIContent("Settings");

            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            currentTab = GUILayout.Toolbar(currentTab, toolbarTabs, customSkin.FindStyle("Tab Indicator"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-40);
            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            // Draw toolbar tabs as a button
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 0;

            GUILayout.EndHorizontal();

            var location = serializedObject.FindProperty("m_Location");
            var width = serializedObject.FindProperty("m_Width");
            var softness = serializedObject.FindProperty("m_Softness");
            var color = serializedObject.FindProperty("m_Color");
            var effectMaterial = serializedObject.FindProperty("m_EffectMaterial");
            var animationSpeed = serializedObject.FindProperty("animationSpeed");
            var mainPanelMode = serializedObject.FindProperty("mainPanelMode");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    mainPanelMode.boolValue = GUILayout.Toggle(mainPanelMode.boolValue, new GUIContent("Enable Main Panel Mode"), customSkin.FindStyle("Toggle"));
                    mainPanelMode.boolValue = GUILayout.Toggle(mainPanelMode.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Location"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(location, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Width"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(width, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Softness"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(softness, new GUIContent(""), true);

                    GUILayout.EndHorizontal();

                    if (mainPanelMode.boolValue == false)
                    {
                        GUILayout.Space(2);
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(new GUIContent("Anim Speed"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                        EditorGUILayout.PropertyField(animationSpeed, new GUIContent(""), true);

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Effect Color"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(color, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Effect Material"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(effectMaterial, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}