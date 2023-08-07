using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(SplashScreenManager))]
    public class SplashScreenManagerEditor : Editor
    {
        private SplashScreenManager ssmTarget;
        private int currentTab;

        private void OnEnable()
        {
            ssmTarget = (SplashScreenManager)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("SSM Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

            // Toolbar content
            GUIContent[] toolbarTabs = new GUIContent[3];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");
            toolbarTabs[2] = new GUIContent("Settings");

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
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                currentTab = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 2;

            GUILayout.EndHorizontal();

            var splashScreenTitles = serializedObject.FindProperty("splashScreenTitles");
            var splashScreen = serializedObject.FindProperty("splashScreen");
            var mainPanelParent = serializedObject.FindProperty("mainPanelParent");
            var modalWindowParent = serializedObject.FindProperty("modalWindowParent");
            var transitionHelper = serializedObject.FindProperty("transitionHelper");
            var disableSplashScreen = serializedObject.FindProperty("disableSplashScreen");
            var startDelay = serializedObject.FindProperty("startDelay");
            var onSplashScreenEnd = serializedObject.FindProperty("onSplashScreenEnd");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(12);
                    EditorGUILayout.PropertyField(splashScreenTitles, new GUIContent("Splash Screen Titles"), true);
                    splashScreenTitles.isExpanded = true;

                    GUILayout.EndHorizontal();
                    GUILayout.Space(4);

                    if (ssmTarget.splashScreenTitles.Count != 0 && ssmTarget.splashScreenTitles[ssmTarget.splashScreenTitles.Count - 1] != null)
                    {
                        if (GUILayout.Button("+  Create a new title", customSkin.button))
                        {
                            GameObject go = Instantiate(ssmTarget.splashScreenTitles[1].gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            go.transform.SetParent(ssmTarget.splashScreenTitles[1].transform.parent, false);
                            go.gameObject.name = "New Title";
                            ssmTarget.splashScreenTitles.Add(go.GetComponent<SplashScreenTitle>());
                        }
                    }

                    GUILayout.EndVertical();
                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Splash Screen"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(splashScreen, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Main Panel Parent"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(mainPanelParent, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Modal Window Parent"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(modalWindowParent, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Transition Helper"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(transitionHelper, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    break;

                case 2:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    disableSplashScreen.boolValue = GUILayout.Toggle(disableSplashScreen.boolValue, new GUIContent("Disable Splash Screen"), customSkin.FindStyle("Toggle"));
                    disableSplashScreen.boolValue = GUILayout.Toggle(disableSplashScreen.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Start Delay"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(startDelay, new GUIContent(""), true);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(12);
                    EditorGUILayout.PropertyField(onSplashScreenEnd, new GUIContent("On Splash Screen End"), true);
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}