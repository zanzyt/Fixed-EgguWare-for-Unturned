using System;
using EgguWare.Utilities;
using UnityEngine;

namespace EgguWare.Menu.Windows
{
	public class GUIWindow
	{
		public static void Window(int windowID)
		{
			GUIWindow.scrollPosition3 = GUILayout.BeginScrollView(GUIWindow.scrollPosition3, "SelectedButtonDropdown");
			if (AssetUtilities.GetSkins(false).Count == 0)
			{
				GUILayout.Label("put gui skins in /Unturned/Unturned_Data/GUISkins/", Array.Empty<GUILayoutOption>());
			}
			foreach (string text in AssetUtilities.GetSkins(false))
			{
				string text2 = text;
				if (text2 == G.Settings.MiscOptions.UISkin)
				{
					text2 = "<b>" + text2 + "</b>";
				}
				if (GUILayout.Button(text2, Array.Empty<GUILayoutOption>()))
				{
					AssetUtilities.LoadGUISkinFromName(text);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.Space(2f);
			if (GUILayout.Button("Load Default GUI", Array.Empty<GUILayoutOption>()))
			{
				G.Settings.MiscOptions.UISkin = "";
				AssetUtilities.Skin = AssetUtilities.VanillaSkin;
			}
			if (GUILayout.Button("Close Window", Array.Empty<GUILayoutOption>()))
			{
				GUIWindow.GUISkinMenuOpen = !GUIWindow.GUISkinMenuOpen;
			}
			GUI.DragWindow();
		}

		private static Vector2 scrollPosition3 = new Vector2(0f, 0f);

		public static bool GUISkinMenuOpen = false;
	}
}
