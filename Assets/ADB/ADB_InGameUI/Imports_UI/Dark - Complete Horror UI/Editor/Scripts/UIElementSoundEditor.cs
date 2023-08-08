using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(UIElementSound))]
    public class UIElementSoundEditor : Editor
    {
        private UIElementSound uisTarget;
        private int currentTab;

        private void OnEnable()
        {
            uisTarget = (UIElementSound)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("UIS Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

            // Toolbar content
            GUIContent[] toolbarTabs = new GUIContent[2];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Settings");

            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            currentTab = GUILayout.Toolbar(currentTab, toolbarTabs, customSkin.FindStyle("Tab Indicator"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-40);
            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            // Draw toolbar tabs as a button
            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 1;

            GUILayout.EndHorizontal();

            var audioSource = serializedObject.FindProperty("audioSource");
            var hoverSound = serializedObject.FindProperty("hoverSound");
            var clickSound = serializedObject.FindProperty("clickSound");
            var enableHoverSound = serializedObject.FindProperty("enableHoverSound");
            var enableClickSound = serializedObject.FindProperty("enableClickSound");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Audio Source"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(audioSource, new GUIContent(""), true);

                    GUILayout.EndHorizontal();

                    if (enableHoverSound.boolValue == true)
                    {
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Hover Sound"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                        EditorGUILayout.PropertyField(hoverSound, new GUIContent(""), true);

                        GUILayout.EndHorizontal();
                    }

                    if (enableClickSound.boolValue == true)
                    {
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Click Sound"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                        EditorGUILayout.PropertyField(clickSound, new GUIContent(""), true);

                        GUILayout.EndHorizontal();
                    }

                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableHoverSound.boolValue = GUILayout.Toggle(enableHoverSound.boolValue, new GUIContent("Enable Hover Sound"), customSkin.FindStyle("Toggle"));
                    enableHoverSound.boolValue = GUILayout.Toggle(enableHoverSound.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableClickSound.boolValue = GUILayout.Toggle(enableClickSound.boolValue, new GUIContent("Enable Click Sound"), customSkin.FindStyle("Toggle"));
                    enableClickSound.boolValue = GUILayout.Toggle(enableClickSound.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}