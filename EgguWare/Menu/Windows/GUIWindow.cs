using System;
using EgguWare.Utilities;
using UnityEngine;

namespace EgguWare.Menu.Windows
{
	// Token: 0x0200002B RID: 43
	public class GUIWindow
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000071A8 File Offset: 0x000053A8
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

		// Token: 0x040000B1 RID: 177
		private static Vector2 scrollPosition3 = new Vector2(0f, 0f);

		// Token: 0x040000B2 RID: 178
		public static bool GUISkinMenuOpen = false;
	}
}
