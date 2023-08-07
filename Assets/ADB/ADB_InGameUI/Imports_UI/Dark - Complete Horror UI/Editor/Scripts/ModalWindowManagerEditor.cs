using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(ModalWindowManager))]
    public class ModalWindowManagerEditor : Editor
    {
        private ModalWindowManager mwTarget;
        private int currentTab;

        private void OnEnable()
        {
            mwTarget = (ModalWindowManager)target;
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

            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("MW Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

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

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                currentTab = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 2;

            GUILayout.EndHorizontal();

            var title = serializedObject.FindProperty("title");
            var description = serializedObject.FindProperty("description");
            var onEnable = serializedObject.FindProperty("onEnable");
            var onCancel = serializedObject.FindProperty("onCancel");
            var windowIcon = serializedObject.FindProperty("windowIcon");
            var titleObject = serializedObject.FindProperty("titleObject");
            var descriptionObject = serializedObject.FindProperty("descriptionObject");
            var windowAnimator = serializedObject.FindProperty("windowAnimator");
            var enableDissolve = serializedObject.FindProperty("enableDissolve");
            var dissolveEffect = serializedObject.FindProperty("dissolveEffect");
            var useCustomContent = serializedObject.FindProperty("useCustomContent");
            var disableOnOut = serializedObject.FindProperty("disableOnOut");
            var disableAfter = serializedObject.FindProperty("disableAfter");
            var blurManager = serializedObject.FindProperty("blurManager");

            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Title"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(title, new GUIContent(""));

                    GUILayout.EndHorizontal();

                    if (mwTarget.titleObject != null && useCustomContent.boolValue == false)
                        mwTarget.titleObject.text = title.stringValue;

                    else if (mwTarget.titleObject == null)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("'Title Object' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Description"), customSkin.FindStyle("Text"), GUILayout.Width(-3));
                    EditorGUILayout.PropertyField(description, new GUIContent(""), GUILayout.Height(80));

                    GUILayout.EndHorizontal();

                    if (mwTarget.descriptionObject != null && useCustomContent.boolValue == false)
                        mwTarget.descriptionObject.text = description.stringValue;

                    else if (mwTarget.descriptionObject == null)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("'Description Object' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                        GUILayout.EndHorizontal();
                    }

                    if (mwTarget.GetComponent<CanvasGroup>().alpha == 0)
                    {
                        if (GUILayout.Button("Make It Visible", customSkin.button))
                        {
                            mwTarget.GetComponent<CanvasGroup>().alpha = 1;
                            Undo.RegisterCreatedObjectUndo(mwTarget, "Modal set visible");
                        }
                    }

                    else
                    {
                        if (GUILayout.Button("Make It Invisible", customSkin.button))
                        {
                            mwTarget.GetComponent<CanvasGroup>().alpha = 0;
                            Undo.RegisterCreatedObjectUndo(mwTarget, "Modal set invisible");
                        }
                    }

                    GUILayout.Space(10);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Events Header"));
                    EditorGUILayout.PropertyField(onEnable, new GUIContent("On Confirm"), true);
                    EditorGUILayout.PropertyField(onCancel, new GUIContent("On Cancel"), true);
                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Title Object"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(titleObject, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Description Object"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(descriptionObject, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Blur Manager"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(blurManager, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Animator"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(windowAnimator, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    useCustomContent.boolValue = GUILayout.Toggle(useCustomContent.boolValue, new GUIContent("Use Custom Content"), customSkin.FindStyle("Toggle"));
                    useCustomContent.boolValue = GUILayout.Toggle(useCustomContent.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    GUILayout.BeginHorizontal();

                    enableDissolve.boolValue = GUILayout.Toggle(enableDissolve.boolValue, new GUIContent("Enable Dissolve Effect"), customSkin.FindStyle("Toggle"));
                    enableDissolve.boolValue = GUILayout.Toggle(enableDissolve.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.Space(4);

                    if (disableOnOut.boolValue == true)
                    {
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(new GUIContent("Dissolve Effect"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                        EditorGUILayout.PropertyField(dissolveEffect, new GUIContent(""));

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    GUILayout.BeginHorizontal();

                    disableOnOut.boolValue = GUILayout.Toggle(disableOnOut.boolValue, new GUIContent("Disable On Close"), customSkin.FindStyle("Toggle"));
                    disableOnOut.boolValue = GUILayout.Toggle(disableOnOut.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.Space(4);

                    if (disableOnOut.boolValue == true)
                    {
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(new GUIContent("Disable After"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                        EditorGUILayout.PropertyField(disableAfter, new GUIContent(""));

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}