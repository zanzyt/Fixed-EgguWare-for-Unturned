using System;
using System.Collections.Generic;
using EgguWare.Utilities;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{
	public class SettingsTab
	{
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 260f, 400f), "Colors", "box");
			if (G.Settings.GlobalOptions.GlobalColors.Count > 0)
			{
				SettingsTab.scrollPosition1 = GUILayout.BeginScrollView(SettingsTab.scrollPosition1, Array.Empty<GUILayoutOption>());
				List<string> list = new List<string>(G.Settings.GlobalOptions.GlobalColors.Keys);
				for (int i = 0; i < list.Count; i++)
				{
					string text = list[i];
					Color32 color = Colors.GetColor(text);
					string text2 = string.Concat(new string[]
					{
						"<color=#",
						Colors.ColorToHex(color),
						">",
						text.Replace("_", " "),
						"</color>"
					});
					if (SettingsTab.SelectedColorIdentifier == text)
					{
						if (GUILayout.Button(text2, "SelectedButton", Array.Empty<GUILayoutOption>()))
						{
							SettingsTab.SelectedColorIdentifier = "";
						}
						GUILayout.BeginVertical("SelectedButtonDropdown", Array.Empty<GUILayoutOption>());
						Color32 color2 = color;
						Color32 color3 = new Color32
						{
							a = byte.MaxValue
						};
						GUILayout.Label("R: " + color2.r.ToString(), Array.Empty<GUILayoutOption>());
						color3.r = (byte)GUILayout.HorizontalSlider((float)color2.r, 0f, 255f, Array.Empty<GUILayoutOption>());
						GUILayout.Space(2f);
						GUILayout.Label("G: " + color2.g.ToString(), Array.Empty<GUILayoutOption>());
						color3.g = (byte)GUILayout.HorizontalSlider((float)color2.g, 0f, 255f, Array.Empty<GUILayoutOption>());
						GUILayout.Space(2f);
						GUILayout.Label("B: " + color2.b.ToString(), Array.Empty<GUILayoutOption>());
						color3.b = (byte)GUILayout.HorizontalSlider((float)color2.b, 0f, 255f, Array.Empty<GUILayoutOption>());
						G.Settings.GlobalOptions.GlobalColors[text] = color3;
						GUILayout.EndVertical();
					}
					else if (GUILayout.Button(text2, Array.Empty<GUILayoutOption>()))
					{
						SettingsTab.SelectedColorIdentifier = text;
					}
				}
				GUILayout.EndScrollView();
			}
			GUILayout.EndArea();
			Rect rect = new Rect(280f, 35f, 260f, 400f);
			GUIStyle guistyle = "box";
			GUILayout.BeginArea(rect, "Config: <b>" + ConfigUtilities.SelectedConfig + "</b>", guistyle);
			SettingsTab.textfield = GUILayout.TextField(SettingsTab.textfield, Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("Create Config", Array.Empty<GUILayoutOption>()) && !string.IsNullOrEmpty(SettingsTab.textfield))
			{
				ConfigUtilities.SaveConfig(SettingsTab.textfield, true);
				SettingsTab.textfield = "";
			}
			if (GUILayout.Button("Save Current Config", Array.Empty<GUILayoutOption>()))
			{
				ConfigUtilities.SaveConfig(ConfigUtilities.SelectedConfig, false);
			}
			GUILayout.Space(5f);
			SettingsTab.scrollPosition2 = GUILayout.BeginScrollView(SettingsTab.scrollPosition2, "SelectedButtonDropdown");
			foreach (string text3 in ConfigUtilities.GetConfigs(false))
			{
				if (text3 == ConfigUtilities.SelectedConfig)
				{
					text3 = "<b>" + text3 + "</b>";
				}
				if (GUILayout.Button(text3, Array.Empty<GUILayoutOption>()))
				{
					ConfigUtilities.LoadConfig(text3);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		public static string SelectedColorIdentifier = "";

		private static Vector2 scrollPosition1 = new Vector2(0f, 0f);

		private static string textfield = "New Config";

		private static Vector2 scrollPosition2 = new Vector2(0f, 0f);
	}
}
