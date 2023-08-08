using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(UIManager))]
    [System.Serializable]
    public class UIManagerEditor : Editor
    {
        Texture2D duimLogo;
        protected static bool showBackground = false;
        protected static bool showColors = false;
        protected static bool showFonts = false;
        protected static bool showLogo = false;

        void OnEnable()
        {
            if (EditorGUIUtility.isProSkin == true)
                duimLogo = Resources.Load<Texture2D>("Editor\\DUIM Editor Dark");
            else
                duimLogo = Resources.Load<Texture2D>("Editor\\DUIM Editor Light");
        }

        public override void OnInspectorGUI()
        {
            // GUI skin variables
            GUISkin customSkin;

            if (EditorGUIUtility.isProSkin == true)
                customSkin = (GUISkin)Resources.Load("Editor\\DUI Skin Dark");
            else
                customSkin = (GUISkin)Resources.Load("Editor\\DUI Skin Light");

            // Foldout style
            GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
            foldoutStyle.font = customSkin.font;
            foldoutStyle.fontStyle = FontStyle.Normal;
            foldoutStyle.fontSize = 15;
            foldoutStyle.margin = new RectOffset(11, 55, 6, 6);
            Vector2 contentOffset = foldoutStyle.contentOffset;
            contentOffset.x = 5;
            foldoutStyle.contentOffset = contentOffset;

            // Logo
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(duimLogo, GUILayout.Width(250), GUILayout.Height(40));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(6);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            // Background
            var backgroundType = serializedObject.FindProperty("backgroundType");
            var backgroundImage = serializedObject.FindProperty("backgroundImage");
            var backgroundPreserveAspect = serializedObject.FindProperty("backgroundPreserveAspect");
            var backgroundVideo = serializedObject.FindProperty("backgroundVideo");
            var backgroundSpeed = serializedObject.FindProperty("backgroundSpeed");
            var backgroundColorTint = serializedObject.FindProperty("backgroundColorTint");
            showBackground = EditorGUILayout.Foldout(showBackground, "Background", true, foldoutStyle);

            if (showBackground && backgroundType.enumValueIndex == 0)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Background Image"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundImage, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Color Tint"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundColorTint, new GUIContent(""));

                GUILayout.EndHorizontal();
            }

            if (showBackground && backgroundType.enumValueIndex == 1)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Background Type"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundType, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Background Video"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundVideo, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Color Tint"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundColorTint, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Animation Speed"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundSpeed, new GUIContent(""));

                GUILayout.EndHorizontal();
                EditorGUILayout.HelpBox("Video Player will be used for background on Advanced mode.", MessageType.Info);
            }

            GUILayout.EndVertical();
            GUILayout.Space(2);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            // Colors       
            var primaryColor = serializedObject.FindProperty("primaryColor");
            var secondaryColor = serializedObject.FindProperty("secondaryColor");
            var primaryReversed = serializedObject.FindProperty("primaryReversed");
            var negativeColor = serializedObject.FindProperty("negativeColor");
            var backgroundColor = serializedObject.FindProperty("backgroundColor");
            var backgroundColorAlt = serializedObject.FindProperty("backgroundColorAlt");
            showColors = EditorGUILayout.Foldout(showColors, "Colors", true, foldoutStyle);

            if (showColors)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Primary Color"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(primaryColor, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Primary Reversed"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(primaryReversed, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Secondary Color"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(secondaryColor, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Negative Color"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(negativeColor, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Background Color"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundColor, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Background Color Alt"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(backgroundColorAlt, new GUIContent(""));

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.Space(2);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            // Fonts
            var lightFont = serializedObject.FindProperty("lightFont");
            var mediumFont = serializedObject.FindProperty("mediumFont");
            var boldFont = serializedObject.FindProperty("boldFont");
            var altFont = serializedObject.FindProperty("altFont");
            var alt2Font = serializedObject.FindProperty("alt2Font");
            showFonts = EditorGUILayout.Foldout(showFonts, "Fonts", true, foldoutStyle);

            if (showFonts)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Light Font"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(lightFont, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Medium Font"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(mediumFont, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Bold Font"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(boldFont, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Alternative Font"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(altFont, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Alternative 2 Font"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(alt2Font, new GUIContent(""));

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.Space(2);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            // Logo
            var brandLogo = serializedObject.FindProperty("brandLogo");
            var gameLogo = serializedObject.FindProperty("gameLogo");
            var logoColor = serializedObject.FindProperty("logoColor");
            showLogo = EditorGUILayout.Foldout(showLogo, "Logo", true, foldoutStyle);

            if (showLogo)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Brand Logo"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(brandLogo, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Game Logo"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(gameLogo, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                EditorGUILayout.LabelField(new GUIContent("Logo Color"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                EditorGUILayout.PropertyField(logoColor, new GUIContent(""));

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.Space(7);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(6);

            var enableDynamicUpdate = serializedObject.FindProperty("enableDynamicUpdate");

            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            enableDynamicUpdate.boolValue = GUILayout.Toggle(enableDynamicUpdate.boolValue, new GUIContent("Update Values"), customSkin.FindStyle("Toggle"));
            enableDynamicUpdate.boolValue = GUILayout.Toggle(enableDynamicUpdate.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

            GUILayout.EndHorizontal();

            var enableExtendedColorPicker = serializedObject.FindProperty("enableExtendedColorPicker");

            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            enableExtendedColorPicker.boolValue = GUILayout.Toggle(enableExtendedColorPicker.boolValue, new GUIContent("Extended Color Picker"), customSkin.FindStyle("Toggle"));
            enableExtendedColorPicker.boolValue = GUILayout.Toggle(enableExtendedColorPicker.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

            GUILayout.EndHorizontal();

            if (enableExtendedColorPicker.boolValue == true)
                EditorPrefs.SetInt("DarkUIManager.EnableExtendedColorPicker", 1);
            else
                EditorPrefs.SetInt("DarkUIManager.EnableExtendedColorPicker", 0);

            var editorHints = serializedObject.FindProperty("editorHints");

            GUILayout.BeginVertical(EditorStyles.helpBox);

            editorHints.boolValue = GUILayout.Toggle(editorHints.boolValue, new GUIContent("UI Manager Hints"), customSkin.FindStyle("Toggle"));

            if (editorHints.boolValue == true)
            {
                EditorGUILayout.HelpBox("These values are universal and will affect any object that contains 'UI Manager' component.", MessageType.Info);
                EditorGUILayout.HelpBox("Remove 'UI Manager' component from the object in order to get unique values.", MessageType.Info);
            }

            GUILayout.EndVertical();

            var rootFolder = serializedObject.FindProperty("rootFolder");
            var changeRootFolder = serializedObject.FindProperty("changeRootFolder");

            GUILayout.BeginVertical(EditorStyles.helpBox);

            changeRootFolder.boolValue = GUILayout.Toggle(changeRootFolder.boolValue, new GUIContent("Change Root Folder"), customSkin.FindStyle("Toggle"), GUILayout.Width(500));

            if (changeRootFolder.boolValue == true)
            {
                EditorGUI.indentLevel = 2;
                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(new GUIContent("Root Folder:"), customSkin.FindStyle("Text"), GUILayout.Width(76));
                EditorGUILayout.PropertyField(rootFolder, new GUIContent(""));

                GUILayout.EndHorizontal();
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Set Default", customSkin.button, GUILayout.Width(120)))
                {
                    rootFolder.stringValue = "Dark - Complete Horror UI/Prefabs";
                    EditorPrefs.SetString("DarkUI.ObjectCreator.RootFolder", rootFolder.stringValue);
                }

                if (GUILayout.Button("Apply", customSkin.button, GUILayout.Width(120)))
                    EditorPrefs.SetString("DarkUI.ObjectCreator.RootFolder", rootFolder.stringValue);

                GUILayout.EndHorizontal();
                GUILayout.Space(2);
                EditorGUI.indentLevel = 0;
                EditorGUILayout.HelpBox("Make sure that the Dark UI directory matches. " +
                    "Don't forget to hit apply after changing the root. " +
                    "Example: Parent Folders/Dark - Complete Horror UI/Prefabs.", MessageType.Warning);
            }

            GUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

            GUILayout.Space(12);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Reset to defaults", customSkin.button))
                ResetToDefaults();

            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUILayout.Label("Need help? Contact me via:", customSkin.FindStyle("Text"));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("E-mail", customSkin.button))
                Email();

            if (GUILayout.Button("Twitter", customSkin.button))
                Twitter();

            if (GUILayout.Button("Discord", customSkin.button))
                Discord();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(6);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("ID: D16-20211020");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(6);
        }

        void Discord()
        {
            Application.OpenURL("https://discord.gg/VXpHyUt");
        }

        void Email()
        {
            Application.OpenURL("https://www.michsky.com/contact");
        }

        void Twitter()
        {
            Application.OpenURL("https://twitter.com/michskyHQ");
        }

        void ResetToDefaults()
        {
            if (EditorUtility.DisplayDialog("Reset to defaults", "Are you sure you want to reset Dark UI Manager values to default?", "Yes", "Cancel"))
            {
                try
                {
                    Preset defaultPreset = Resources.Load<Preset>("DUIM Presets/Default");
                    defaultPreset.ApplyTo(Resources.Load("Dark UI Manager"));
                    Selection.activeObject = null;
                    Debug.Log("<b>[Dark UI Manager]</b> Resetting successful.");
                }

                catch { Debug.LogWarning("<b>[Dark UI Manager]</b> Resetting failed. Default preset is probably missing."); }
            }
        }
    }
}