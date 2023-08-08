using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(BackgroundFilter))]
    public class BackgroundFilterEditor : Editor
    {
        private BackgroundFilter bfTarget;
        List<string> filterList = new List<string>();
        private int currentTab;

        private void OnEnable()
        {
            bfTarget = (BackgroundFilter)target;

            foreach (var t in bfTarget.filterList)
                filterList.Add(t.name);
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Filter Top Header"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-42);

            // Toolbar content
            GUIContent[] toolbarTabs = new GUIContent[1];
            toolbarTabs[0] = new GUIContent("Options");

            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            currentTab = GUILayout.Toolbar(currentTab, toolbarTabs, customSkin.FindStyle("Tab Indicator"));

            GUILayout.EndHorizontal();
            GUILayout.Space(-40);
            GUILayout.BeginHorizontal();
            GUILayout.Space(17);

            // Draw toolbar tabs as a button
            if (GUILayout.Button(new GUIContent("Customize", "Customize"), customSkin.FindStyle("Tab Settings")))
                currentTab = 0;

            GUILayout.EndHorizontal();

            var selectedFilter = serializedObject.FindProperty("selectedFilter");
            var filterIntensity = serializedObject.FindProperty("filterIntensity");
            var filterImage = serializedObject.FindProperty("filterImage");
            var editMode = serializedObject.FindProperty("editMode");
            var filterListMain = serializedObject.FindProperty("filterList");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Selected Filter"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    selectedFilter.intValue = EditorGUILayout.Popup(selectedFilter.intValue, filterList.ToArray());
                    bfTarget.filterImage.sprite = bfTarget.filterList[bfTarget.selectedFilter];
                    bfTarget.filterImage.color = new Color(bfTarget.filterImage.color.r, bfTarget.filterImage.color.g, bfTarget.filterImage.color.b, bfTarget.filterIntensity);

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(new GUIContent("Filter Intensity"), customSkin.FindStyle("Text"), GUILayout.Width(100));
                    EditorGUILayout.PropertyField(filterIntensity, new GUIContent(""), true);

                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Update", customSkin.button))
                    {
                        bfTarget.gameObject.SetActive(false);
                        bfTarget.gameObject.SetActive(true);
                    }

                    GUILayout.EndVertical();
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    GUILayout.BeginHorizontal();

                    editMode.boolValue = GUILayout.Toggle(editMode.boolValue, new GUIContent("Edit Mode"), customSkin.FindStyle("Toggle"));
                    editMode.boolValue = GUILayout.Toggle(editMode.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.Space(3);

                    if (editMode.boolValue == true)
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);

                        EditorGUILayout.PropertyField(filterImage, new GUIContent("Filter Source"), true);

                        GUILayout.BeginHorizontal();
                        GUILayout.Space(12);

                        EditorGUILayout.PropertyField(filterListMain, new GUIContent("Filters"), true);

                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                    }

                    GUILayout.EndVertical();
                    break;
            }

            this.Repaint();

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}