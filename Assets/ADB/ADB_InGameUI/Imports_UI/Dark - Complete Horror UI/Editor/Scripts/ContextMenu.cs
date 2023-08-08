using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Michsky.UI.Dark
{
    public class ContextMenu : Editor
    {
        static void CreateObject(string resourcePath)
        {
            try
            {
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/" + EditorPrefs.GetString("DarkUI.ObjectCreator.RootFolder") + resourcePath + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                try
                {
                    if (Selection.activeGameObject == null)
                    {
                        var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                        Undo.RegisterCreatedObjectUndo(clone, "Created an object");
                        clone.transform.SetParent(canvas.transform, false);
                    }

                    else
                    {
                        Undo.RegisterCreatedObjectUndo(clone, "Created an object");
                        clone.transform.SetParent(Selection.activeGameObject.transform, false);
                    }

                    clone.name = clone.name.Replace("(Clone)", "").Trim();
                }

                catch
                {
                    Undo.RegisterCreatedObjectUndo(clone, "Created an object");
                    CreateCanvas();
                    var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                    clone.transform.SetParent(canvas.transform, false);
                    clone.name = clone.name.Replace("(Clone)", "").Trim();
                }

                Selection.activeObject = clone;
            }

            catch
            {
                if (EditorUtility.DisplayDialog("Dark UI", "Cannot create the object due to missing/incorrect root folder. " +
                    "You can change the root folder by clicking 'Fix' button and enabling 'Change Root Folder'.", "Fix", "Cancel"))
                    ShowManager();
            }

            if (Application.isPlaying == false)
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("GameObject/Dark UI/Canvas", false, -1)]
        static void CreateCanvas()
        {
            try
            {
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/" + EditorPrefs.GetString("DarkUI.ObjectCreator.RootFolder") + "UI Elements/Canvas" + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                Undo.RegisterCreatedObjectUndo(clone, "Created an object");
                clone.name = clone.name.Replace("(Clone)", "").Trim();
                Selection.activeObject = clone;
            }

            catch
            {
                if (EditorUtility.DisplayDialog("Dark UI", "Cannot create the object due to missing/incorrect root folder. " +
                  "You can change the root folder by clicking 'Fix' button and enabling 'Change Root Folder'.", "Fix", "Cancel"))
                    ShowManager();
            }

            if (Application.isPlaying == false)
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("Tools/Dark UI/Show UI Manager")]
        static void ShowManager()
        {
            Selection.activeObject = Resources.Load("Dark UI Manager");

            if (Selection.activeObject == null)
                Debug.Log("<b>[Dark UI]</b>Can't find a file named 'Dark UI Manager'. Make sure you have 'Dark UI Manager' file in Resources folder.");
        }

        [MenuItem("GameObject/Dark UI/Chapter Button", false, 0)]
        static void UIECB()
        {
            CreateObject("UI Elements/Chapter Button");
        }

        [MenuItem("GameObject/Dark UI/Dropdown", false, 0)]
        static void UIEDR()
        {
            CreateObject("UI Elements/Dropdown");
        }

        [MenuItem("GameObject/Dark UI/Horizontal Selector", false, 0)]
        static void UIEHS()
        {
            CreateObject("UI Elements/Horizontal Selector");
        }

        [MenuItem("GameObject/Dark UI/Load Save Button", false, 0)]
        static void UIELSB()
        {
            CreateObject("UI Elements/Load Save Button");
        }

        [MenuItem("GameObject/Dark UI/Main Button", false, 0)]
        static void UIEMB()
        {
            CreateObject("UI Elements/Main Button");
        }

        [MenuItem("GameObject/Dark UI/Main Button (Free)", false, 0)]
        static void UIEMBF()
        {
            CreateObject("UI Elements/Main Button (Free)");
        }

        [MenuItem("GameObject/Dark UI/Modal Window", false, 0)]
        static void UIEMW()
        {
            CreateObject("UI Elements/Modal Window");
        }

        [MenuItem("GameObject/Dark UI/Radial Button", false, 0)]
        static void UIERB()
        {
            CreateObject("UI Elements/Radial Button");
        }

        [MenuItem("GameObject/Dark UI/Scrollbar", false, 0)]
        static void UIESCR()
        {
            CreateObject("UI Elements/Scrollbar");
        }

        [MenuItem("GameObject/Dark UI/Server Button", false, 0)]
        static void UIESB()
        {
            CreateObject("UI Elements/Server Button");
        }

        [MenuItem("GameObject/Dark UI/Shortcut Key", false, 0)]
        static void UIESK()
        {
            CreateObject("UI Elements/Shortcut Key");
        }

        [MenuItem("GameObject/Dark UI/Shortcut Key (Gamepad)", false, 0)]
        static void UIESKG()
        {
            CreateObject("UI Elements/Shortcut Key (Gamepad)");
        }

        [MenuItem("GameObject/Dark UI/Slider", false, 0)]
        static void UIESLI()
        {
            CreateObject("UI Elements/Slider");
        }

        [MenuItem("GameObject/Dark UI/Switch", false, 0)]
        static void UIESWI()
        {
            CreateObject("UI Elements/Switch");
        }

        [MenuItem("GameObject/Dark UI/Tab Button", false, 0)]
        static void UIETABB()
        {
            CreateObject("UI Elements/Tab Button");
        }
    }
}
#endif