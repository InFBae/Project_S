using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(GamepadChecker))]
    public class GamepadCheckerEditor : Editor
    {
        private GamepadChecker gcTarget;
        private int currentTab;

        private void OnEnable()
        {
            gcTarget = (GamepadChecker)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("GM Top Header"));

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

            var defaultPanelManager = serializedObject.FindProperty("defaultPanelManager");
            var panelManagers = serializedObject.FindProperty("panelManagers");
            var alwaysUpdate = serializedObject.FindProperty("alwaysUpdate");
            var affectCursor = serializedObject.FindProperty("affectCursor");
            var gamepadHotkey = serializedObject.FindProperty("gamepadHotkey");
            var keyboardObjects = serializedObject.FindProperty("keyboardObjects");
            var gamepadObjects = serializedObject.FindProperty("gamepadObjects");
            var buttons = serializedObject.FindProperty("buttons");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Default Panel Manager"), customSkin.FindStyle("Text"), GUILayout.Width(140));
                    EditorGUILayout.PropertyField(defaultPanelManager, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    GUILayout.Space(12);

                    EditorGUILayout.PropertyField(panelManagers, new GUIContent("Panel Managers"), true);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    GUILayout.Space(12);

                    EditorGUILayout.PropertyField(keyboardObjects, new GUIContent("Keyboard Objects"), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    GUILayout.Space(12);

                    EditorGUILayout.PropertyField(gamepadObjects, new GUIContent("Gamepad Objects"), true);
                 
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    GUILayout.Space(12);

                    EditorGUILayout.PropertyField(buttons, new GUIContent("Button Objects"), true);

                    GUILayout.EndHorizontal();
                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    alwaysUpdate.boolValue = GUILayout.Toggle(alwaysUpdate.boolValue, new GUIContent("Always Update"), customSkin.FindStyle("Toggle"));
                    alwaysUpdate.boolValue = GUILayout.Toggle(alwaysUpdate.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    affectCursor.boolValue = GUILayout.Toggle(affectCursor.boolValue, new GUIContent("Affect Cursor"), customSkin.FindStyle("Toggle"));
                    affectCursor.boolValue = GUILayout.Toggle(affectCursor.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.PropertyField(gamepadHotkey, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}