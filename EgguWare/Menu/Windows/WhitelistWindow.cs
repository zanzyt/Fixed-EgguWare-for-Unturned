using System;
using EgguWare.Cheats;
using EgguWare.Classes;
using UnityEngine;

namespace EgguWare.Menu.Windows
{
	public class WhitelistWindow
	{
		public static void Window(int windowID)
		{
			ItemWhitelistObject itemWhitelistObject = (Items.editingaip ? G.Settings.MiscOptions.AIPWhitelist : G.Settings.MiscOptions.ESPWhitelist);
			string text = (Items.editingaip ? "Pickup" : "Display");
			itemWhitelistObject.filterItems = GUILayout.Toggle(itemWhitelistObject.filterItems, "Filter Item Whitelist", Array.Empty<GUILayoutOption>());
			if (itemWhitelistObject.filterItems)
			{
				GUILayout.Space(3f);
				GUILayout.BeginVertical("SelectedButtonDropdown", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowGun = GUILayout.Toggle(itemWhitelistObject.allowGun, text + " Guns", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowMelee = GUILayout.Toggle(itemWhitelistObject.allowMelee, text + " Melees", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowBackpack = GUILayout.Toggle(itemWhitelistObject.allowBackpack, text + " Backpacks", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowClothing = GUILayout.Toggle(itemWhitelistObject.allowClothing, text + " Clothing", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowFuel = GUILayout.Toggle(itemWhitelistObject.allowFuel, text + " Fuel", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowFoodWater = GUILayout.Toggle(itemWhitelistObject.allowFoodWater, text + " Food/Water", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowAmmo = GUILayout.Toggle(itemWhitelistObject.allowAmmo, text + " Ammo", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowMedical = GUILayout.Toggle(itemWhitelistObject.allowMedical, text + " Medical", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowThrowable = GUILayout.Toggle(itemWhitelistObject.allowThrowable, text + " Throwables", Array.Empty<GUILayoutOption>());
				itemWhitelistObject.allowAttachments = GUILayout.Toggle(itemWhitelistObject.allowAttachments, text + " Attachments", Array.Empty<GUILayoutOption>());
				GUILayout.EndVertical();
			}
			if (GUILayout.Button("Close Window", Array.Empty<GUILayoutOption>()))
			{
				WhitelistWindow.WhitelistMenuOpen = !WhitelistWindow.WhitelistMenuOpen;
			}
			GUI.DragWindow();
		}

		public static bool WhitelistMenuOpen;
	}
}
