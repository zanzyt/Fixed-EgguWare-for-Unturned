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
	[Comp]
	public class Main : MonoBehaviour
	{
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

		public static MenuTab SelectedTab = MenuTab.Visuals;

		public static bool MenuOpen = false;

		public static string DropdownTitle;

		public static Rect DropdownPos;

		public static Color GUIColor;

		private List<GUIContent> buttons;

		public static List<GUIContent> buttons2 = new List<GUIContent>();

		public static List<GUIContent> buttons3 = new List<GUIContent>();

		public static List<GUIContent> buttons4 = new List<GUIContent>();

		public static List<GUIContent> buttons5 = new List<GUIContent>();

		public static Rect CursorPos = new Rect(0f, 0f, 20f, 20f);

		private int i;

		private Texture _cursorTexture;

		private Rect windowRect;

		private Rect itemRect;

		private Rect guiRect;

		private readonly string Name;

		private readonly string Version;
		
		private Font dynamicFont;
	}
}
