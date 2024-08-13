using System;
using System.Collections.Generic;
using EgguWare.Attributes;
using EgguWare.Cheats;
using EgguWare.Classes;
using EgguWare.Menu.Tabs;
using EgguWare.Menu.Windows;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Menu
{
	// Token: 0x0200002A RID: 42
	[Comp]
	public class Main : MonoBehaviour
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00006B80 File Offset: 0x00004D80
		private void Start()
		{
			Main.GUIColor = GUI.color;
			foreach (object obj in Enum.GetValues(typeof(MenuTab)))
			{
				MenuTab menuTab = (MenuTab)obj;
				this.buttons.Add(new GUIContent(Enum.GetName(typeof(MenuTab), menuTab)));
			}
			foreach (object obj2 in Enum.GetValues(typeof(ESPObject)))
			{
				ESPObject espobject = (ESPObject)obj2;
				Main.buttons2.Add(new GUIContent(Enum.GetName(typeof(ESPObject), espobject)));
			}
			foreach (object obj3 in Enum.GetValues(typeof(MiscOptions)))
			{
				MiscOptions miscOptions = (MiscOptions)obj3;
				Main.buttons3.Add(new GUIContent(Enum.GetName(typeof(MiscOptions), miscOptions).Replace("_", " ")));
			}
			foreach (object obj4 in Enum.GetValues(typeof(SettingsOptions)))
			{
				SettingsOptions settingsOptions = (SettingsOptions)obj4;
				Main.buttons4.Add(new GUIContent(Enum.GetName(typeof(SettingsOptions), settingsOptions)));
			}
			foreach (object obj5 in Enum.GetValues(typeof(AimbotOptions)))
			{
				AimbotOptions aimbotOptions = (AimbotOptions)obj5;
				Main.buttons5.Add(new GUIContent(Enum.GetName(typeof(AimbotOptions), aimbotOptions)));
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000024D3 File Offset: 0x000006D3
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F1))
			{
				if (!Main.MenuOpen)
				{
					Main.MenuOpen = true;
					return;
				}
				Main.MenuOpen = false;
				this.i = -40;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006DD8 File Offset: 0x00004FD8
		private void OnGUI()
		{
			if (!G.BeingSpied && Main.MenuOpen)
			{
				GUI.skin = AssetUtilities.Skin;
				if (this._cursorTexture == null)
				{
					this._cursorTexture = Resources.Load("UI/Glazier_IMGUI/Cursor") as Texture;
				}
				GUI.depth = -1;
				GUIStyle guistyle = new GUIStyle("label");
				guistyle.margin = new RectOffset(10, 10, 5, 5);
				guistyle.fontSize = 22;
				if (this.i < 0)
				{
					this.i++;
				}
				this.windowRect = GUILayout.Window(0, this.windowRect, new GUI.WindowFunction(this.MenuWindow), Enum.GetName(typeof(MenuTab), Main.SelectedTab), Array.Empty<GUILayoutOption>());
				if (WhitelistWindow.WhitelistMenuOpen)
				{
					this.itemRect = GUILayout.Window(4, this.itemRect, new GUI.WindowFunction(WhitelistWindow.Window), (EgguWare.Cheats.Items.editingaip ? "Pickup " : "ESP ") + "Whitelist", Array.Empty<GUILayoutOption>());
				}
				if (GUIWindow.GUISkinMenuOpen)
				{
					this.guiRect = GUILayout.Window(5, this.guiRect, new GUI.WindowFunction(GUIWindow.Window), "GUI Skins", Array.Empty<GUILayoutOption>());
				}
				GUILayout.BeginArea(new Rect(0f, (float)this.i, (float)Screen.width, 40f), "NavBox");
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUI.color = new Color32(34, 177, 76, byte.MaxValue);
				GUILayout.Label(string.Concat(new string[] { "<b>", this.Name, "</b> <size=15>", this.Version, "</size>" }), guistyle, Array.Empty<GUILayoutOption>());
				GUI.color = Main.GUIColor;
				Main.SelectedTab = (MenuTab)GUILayout.Toolbar((int)Main.SelectedTab, this.buttons.ToArray(), "TabBtn", Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
				GUI.depth = -2;
				Main.CursorPos.x = Input.mousePosition.x;
				Main.CursorPos.y = (float)Screen.height - Input.mousePosition.y;
				GUI.DrawTexture(Main.CursorPos, this._cursorTexture);
				Cursor.lockState = CursorLockMode.None;
				if (PlayerUI.window != null)
				{
					PlayerUI.window.showCursor = true;
				}
				GUI.skin = null;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000704C File Offset: 0x0000524C
		private void MenuWindow(int windowID)
		{
			switch (Main.SelectedTab)
			{
			case MenuTab.Visuals:
				VisualsTab.Tab();
				break;
			case MenuTab.Aimbot:
				AimbotTab.Tab();
				break;
			case MenuTab.Misc:
				MiscTab.Tab();
				break;
			case MenuTab.Weapons:
				WeaponsTab.Tab();
				break;
			case MenuTab.Players:
				PlayersTab.Tab();
				break;
			case MenuTab.Settings:
				SettingsTab.Tab();
				break;
			}
			GUI.DragWindow();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000070AC File Offset: 0x000052AC
		public Main()
		{
			this.buttons = new List<GUIContent>();
			this.i = -40;
			this.windowRect = new Rect(80f, 80f, 550f, 450f);
			this.itemRect = new Rect(400f, 465f, 200f, 250f);
			this.guiRect = new Rect(100f, 755f, 200f, 250f);
			this.Name = "I can use dnspy?";
			this.Version = "Russian vodka";
		}

		// Token: 0x0400009E RID: 158
		public static MenuTab SelectedTab = MenuTab.Visuals;

		// Token: 0x0400009F RID: 159
		public static bool MenuOpen = false;

		// Token: 0x040000A0 RID: 160
		public static string DropdownTitle;

		// Token: 0x040000A1 RID: 161
		public static Rect DropdownPos;

		// Token: 0x040000A2 RID: 162
		public static Color GUIColor;

		// Token: 0x040000A3 RID: 163
		private List<GUIContent> buttons;

		// Token: 0x040000A4 RID: 164
		public static List<GUIContent> buttons2 = new List<GUIContent>();

		// Token: 0x040000A5 RID: 165
		public static List<GUIContent> buttons3 = new List<GUIContent>();

		// Token: 0x040000A6 RID: 166
		public static List<GUIContent> buttons4 = new List<GUIContent>();

		// Token: 0x040000A7 RID: 167
		public static List<GUIContent> buttons5 = new List<GUIContent>();

		// Token: 0x040000A8 RID: 168
		public static Rect CursorPos = new Rect(0f, 0f, 20f, 20f);

		// Token: 0x040000A9 RID: 169
		private int i;

		// Token: 0x040000AA RID: 170
		private Texture _cursorTexture;

		// Token: 0x040000AB RID: 171
		private Rect windowRect;

		// Token: 0x040000AC RID: 172
		private Rect itemRect;

		// Token: 0x040000AD RID: 173
		private Rect guiRect;

		// Token: 0x040000AE RID: 174
		private readonly string Name;

		// Token: 0x040000AF RID: 175
		private readonly string Version;

		// Token: 0x040000B0 RID: 176
		private Font dynamicFont;
	}
}
