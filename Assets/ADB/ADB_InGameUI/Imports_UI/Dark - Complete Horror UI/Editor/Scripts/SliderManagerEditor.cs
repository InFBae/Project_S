using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(SliderManager))]
    public class SliderManagerEditor : Editor
    {
        private SliderManager slmTarget;
        private int currentTab;

        private void OnEnable()
        {
            slmTarget = (SliderManager)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Slider Top Header"));

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

            var valueText = serializedObject.FindProperty("valueText");
            var popupValueText = serializedObject.FindProperty("popupValueText");
            var enableSaving = serializedObject.FindProperty("enableSaving");
            var sliderTag = serializedObject.FindProperty("sliderTag");
            var usePercent = serializedObject.FindProperty("usePercent");
            var showValue = serializedObject.FindProperty("showValue");
            var showPopupValue = serializedObject.FindProperty("showPopupValue");
            var useRoundValue = serializedObject.FindProperty("useRoundValue");
            var valueMultiplier = serializedObject.FindProperty("valueMultiplier");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Value Text"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(valueText, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Popup Value Text"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(popupValueText, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Value Multiplier"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(valueMultiplier, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    usePercent.boolValue = GUILayout.Toggle(usePercent.boolValue, new GUIContent("Use Percent"), customSkin.FindStyle("Toggle"));
                    usePercent.boolValue = GUILayout.Toggle(usePercent.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    showValue.boolValue = GUILayout.Toggle(showValue.boolValue, new GUIContent("Show Value"), customSkin.FindStyle("Toggle"));
                    showValue.boolValue = GUILayout.Toggle(showValue.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    showPopupValue.boolValue = GUILayout.Toggle(showPopupValue.boolValue, new GUIContent("Show Popup Value"), customSkin.FindStyle("Toggle"));
                    showPopupValue.boolValue = GUILayout.Toggle(showPopupValue.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    useRoundValue.boolValue = GUILayout.Toggle(useRoundValue.boolValue, new GUIContent("Use Round Value"), customSkin.FindStyle("Toggle"));
                    useRoundValue.boolValue = GUILayout.Toggle(useRoundValue.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableSaving.boolValue = GUILayout.Toggle(enableSaving.boolValue, new GUIContent("Save Value"), customSkin.FindStyle("Toggle"));
                    enableSaving.boolValue = GUILayout.Toggle(enableSaving.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (enableSaving.boolValue == true)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(35);

                        EditorGUILayout.LabelField(new GUIContent("Slider Tag"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                        EditorGUILayout.PropertyField(sliderTag, new GUIContent(""), true);

                        GUILayout.EndHorizontal();
                        EditorGUILayout.HelpBox("Each slider should has its own unique tag.", MessageType.Info);
                    }

                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}