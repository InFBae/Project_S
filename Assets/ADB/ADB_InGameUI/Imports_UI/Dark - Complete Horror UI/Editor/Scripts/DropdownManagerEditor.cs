using UnityEngine;
using UnityEditor;

namespace Michsky.UI.Dark
{
    [CustomEditor(typeof(DropdownManager))]
    public class DropdownManagerEditor : Editor
    {
        private DropdownManager dTarget;
        private int currentTab;

        private void OnEnable()
        {
            dTarget = (DropdownManager)target;
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
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Dropdown Top Header"));

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

            var triggerObject = serializedObject.FindProperty("triggerObject");
            var selectedText = serializedObject.FindProperty("selectedText");
            var selectedImage = serializedObject.FindProperty("selectedImage");
            var itemParent = serializedObject.FindProperty("itemParent");
            var itemObject = serializedObject.FindProperty("itemObject");
            var scrollbar = serializedObject.FindProperty("scrollbar");
            var itemList = serializedObject.FindProperty("itemList");
            var listParent = serializedObject.FindProperty("listParent");
            var dropdownItems = serializedObject.FindProperty("dropdownItems");
            var dropdownEvent = serializedObject.FindProperty("dropdownEvent");
            var enableIcon = serializedObject.FindProperty("enableIcon");
            var enableTrigger = serializedObject.FindProperty("enableTrigger");
            var enableScrollbar = serializedObject.FindProperty("enableScrollbar");
            var setHighPriorty = serializedObject.FindProperty("setHighPriorty");
            var outOnPointerExit = serializedObject.FindProperty("outOnPointerExit");
            var isListItem = serializedObject.FindProperty("isListItem");
            var invokeAtStart = serializedObject.FindProperty("invokeAtStart");
            var animationType = serializedObject.FindProperty("animationType");
            var selectedItemIndex = serializedObject.FindProperty("selectedItemIndex");
            var saveSelected = serializedObject.FindProperty("saveSelected");
            var dropdownTag = serializedObject.FindProperty("dropdownTag");

            // Draw content depending on tab index
            switch (currentTab)
            {
                case 0:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Content Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    EditorGUI.indentLevel = 1;

                    EditorGUILayout.PropertyField(dropdownItems, new GUIContent("Dropdown Items"), true);
                    dropdownItems.isExpanded = true;

                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("+  Add a new item", customSkin.button))
                        dTarget.AddNewItem();

                    GUILayout.Space(10);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Events Header"));
                    EditorGUILayout.PropertyField(dropdownEvent, new GUIContent("Dropdown Event"), true);

                    break;

                case 1:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Core Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Trigger Object"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(triggerObject, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Selected Text"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(selectedText, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Selected Image"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(selectedImage, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Item Prefab"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(itemObject, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Item Parent"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(itemParent, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Scrollbar"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(scrollbar, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("List Parent"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(listParent, new GUIContent(""));

                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    GUILayout.Space(6);
                    GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Options Header"));
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableIcon.boolValue = GUILayout.Toggle(enableIcon.boolValue, new GUIContent("Enable Icon"), customSkin.FindStyle("Toggle"));
                    enableIcon.boolValue = GUILayout.Toggle(enableIcon.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (dTarget.selectedImage != null)
                    {
                        if (enableIcon.boolValue == true)
                            dTarget.selectedImage.enabled = true;
                        else
                            dTarget.selectedImage.enabled = false;
                    }

                    else
                    {
                        if (enableIcon.boolValue == true)
                        {
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.HelpBox("'Selected Image' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableTrigger.boolValue = GUILayout.Toggle(enableTrigger.boolValue, new GUIContent("Enable Trigger"), customSkin.FindStyle("Toggle"));
                    enableTrigger.boolValue = GUILayout.Toggle(enableTrigger.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (enableTrigger.boolValue == true && dTarget.triggerObject == null)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("'Trigger Object' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    enableScrollbar.boolValue = GUILayout.Toggle(enableScrollbar.boolValue, new GUIContent("Enable Scrollbar"), customSkin.FindStyle("Toggle"));
                    enableScrollbar.boolValue = GUILayout.Toggle(enableScrollbar.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (dTarget.scrollbar != null)
                    {
                        if (enableScrollbar.boolValue == true)
                            dTarget.scrollbar.SetActive(true);
                        else
                            dTarget.scrollbar.SetActive(false);
                    }

                    else
                    {
                        if (enableScrollbar.boolValue == true)
                        {
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.HelpBox("'Scrollbar' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    setHighPriorty.boolValue = GUILayout.Toggle(setHighPriorty.boolValue, new GUIContent("Set High Priorty"), customSkin.FindStyle("Toggle"));
                    setHighPriorty.boolValue = GUILayout.Toggle(setHighPriorty.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    outOnPointerExit.boolValue = GUILayout.Toggle(outOnPointerExit.boolValue, new GUIContent("Out On Pointer Exit"), customSkin.FindStyle("Toggle"));
                    outOnPointerExit.boolValue = GUILayout.Toggle(outOnPointerExit.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    isListItem.boolValue = GUILayout.Toggle(isListItem.boolValue, new GUIContent("Is List Item"), customSkin.FindStyle("Toggle"));
                    isListItem.boolValue = GUILayout.Toggle(isListItem.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (isListItem.boolValue == true && dTarget.listParent == null)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.HelpBox("'List Parent' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    invokeAtStart.boolValue = GUILayout.Toggle(invokeAtStart.boolValue, new GUIContent("Invoke At Start"), customSkin.FindStyle("Toggle"));
                    invokeAtStart.boolValue = GUILayout.Toggle(invokeAtStart.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    saveSelected.boolValue = GUILayout.Toggle(saveSelected.boolValue, new GUIContent("Save Selection"), customSkin.FindStyle("Toggle"));
                    saveSelected.boolValue = GUILayout.Toggle(saveSelected.boolValue, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                    GUILayout.EndHorizontal();

                    if (saveSelected.boolValue == true)
                    {
                        EditorGUI.indentLevel = 2;
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(new GUIContent("Tag:"), customSkin.FindStyle("Text"), GUILayout.Width(40));
                        EditorGUILayout.PropertyField(dropdownTag, new GUIContent(""));

                        GUILayout.EndHorizontal();
                        EditorGUI.indentLevel = 0;
                        EditorGUILayout.HelpBox("Each dropdown should has its own unique tag.", MessageType.Info);
                    }

                    GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.LabelField(new GUIContent("Animation Type"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(animationType, new GUIContent(""));

                    GUILayout.EndHorizontal();

                    if (dTarget.dropdownItems.Count != 0)
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Selected Item Index:"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                        selectedItemIndex.intValue = EditorGUILayout.IntSlider(selectedItemIndex.intValue, 0, dTarget.dropdownItems.Count - 1);

                        GUILayout.Space(2);
                        EditorGUILayout.LabelField(new GUIContent(dTarget.dropdownItems[selectedItemIndex.intValue].itemName), customSkin.FindStyle("Text"));
                        GUILayout.EndVertical();

                        if (saveSelected.boolValue == true)
                            EditorGUILayout.HelpBox("Save Selection is enabled. This option won't be used if there's a stored value.", MessageType.Info);
                    }

                    else
                        EditorGUILayout.HelpBox("There is no item in the list.", MessageType.Warning);
                    break;
            }

            // Apply the changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}