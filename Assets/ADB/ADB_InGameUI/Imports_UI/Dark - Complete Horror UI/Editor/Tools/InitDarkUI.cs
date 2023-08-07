using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine.Rendering;

public class InitDarkUI : MonoBehaviour
{
    [InitializeOnLoad]
	public class InitOnLoad
	{
		static InitOnLoad()
		{
			if (!EditorPrefs.HasKey("DarkUIv2.Installed"))
			{
				EditorPrefs.SetInt("DarkUIv2.Installed", 1);
				EditorUtility.DisplayDialog("Hello there!", "Thank you for purchasing Dark UI. Make sure to import 'Input System' module using Package Manager." +
					"\r\rIf you need help, feel free to contact us through our support channels or Discord. ", "Got it");
			}

			if (!EditorPrefs.HasKey("DarkUI.ObjectCreator.Upgraded"))
			{
				EditorPrefs.SetInt("DarkUI.ObjectCreator.Upgraded", 1);
				EditorPrefs.SetString("DarkUI.ObjectCreator.RootFolder", "Dark UI - Complete Horror UI/Prefabs/");
			}

			if (!EditorPrefs.HasKey("DarkUI.PipelineUpgrader") && GraphicsSettings.renderPipelineAsset != null)
			{
				EditorPrefs.SetInt("DarkUI.PipelineUpgrader", 1);
				
				if (EditorUtility.DisplayDialog("Dark UI SRP Upgrader", "It looks like your project is using URP/HDRP rendering pipeline, " +
					"would you like to upgrade Dark UI Manager for your project?" +
					"\r\rNote that the blur shader currently isn't compatible with URP/HDRP.", "Yes", "No"))
                {
					try
					{
						Preset defaultPreset = Resources.Load<Preset>("DUIM Presets/SRP Default");
						defaultPreset.ApplyTo(Resources.Load("Dark UI Manager"));
					}

					catch { }
				}
			}
		}
	}
}