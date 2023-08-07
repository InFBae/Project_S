using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(QualityManager))]
    public class QualityManagerEditor : Editor
    {
        private QualityManager qmTarget;
        private int currentTab;

        private void OnEnable()
        {
            qmTarget = (QualityManager)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("QM Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

            // Toolbar content
            GUIContent[] toolbarTabs = new GUIContent[2];
            toolbarTabs[0] = new GUIContent("Resources");
            toolbarTabs[1] = new GUIContent("Settings");

            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            currentTab = GUILayout.Toolbar(currentTab, toolbarTabs, customSkin.FindStyle("Tab Indicator"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-40);
            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            // Draw toolbar tabs as a button
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 1;

            GUILayout.EndHorizontal();

            var mixer = serializedObject.FindProperty("mixer");
            var masterSlider = serializedObject.FindProperty("masterSlider");
            var musicSlider = serializedObject.FindProperty("musicSlider");
            var sfxSlider = serializedObject.FindProperty("sfxSlider");
            var customDropdown = serializedObject.FindProperty("customDropdown");
            var defaultDropdown = serializedObject.FindProperty("defaultDropdown");
            var preferCustomDropdown = serializedObject.FindProperty("preferCustomDropdown");
            var clickEvent = serializedObject.FindProperty("clickEvent");
            var isMobile = serializedObject.FindProperty("isMobile");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Audio Mixer"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(mixer, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Master Slider"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(masterSlider, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Music Slider"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(musicSlider, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("SFX Slider"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(sfxSlider, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Default Dropdown"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(defaultDropdown, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUI.enabled = false;
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Custom Dropdown"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(customDropdown, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUI.enabled = true;
                    break;
               
                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUI.enabled = false;
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    preferCustomDropdown.boolValue = GUILayout.Toggle(preferCustomDropdown.boolValue, new GUIContent("Prefer Custom Dropdown"), customSkin.FindStyle("Toggle"));
                    preferCustomDropdown.boolValue = GUILayout.Toggle(preferCustomDropdown.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUI.enabled = true;
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    isMobile.boolValue = GUILayout.Toggle(isMobile.boolValue, new GUIContent("Is Mobile Scene"), customSkin.FindStyle("Toggle"));
                    isMobile.boolValue = GUILayout.Toggle(isMobile.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Events Header"));
                    EditorGUILayout.PropertyField(clickEvent, new GUIContent("Dynamic Res Event"), true);
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}