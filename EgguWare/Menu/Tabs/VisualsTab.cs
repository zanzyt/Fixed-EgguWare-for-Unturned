using System;
using EgguWare.Cheats;
using EgguWare.Classes;
using EgguWare.Menu.Windows;
using EgguWare.Options.ESP;
using EgguWare.Utilities;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{
	public class VisualsTab
	{
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 260f, 400f), "ESP Selection", "box");
			VisualsTab.SelectedObject = (ESPObject)GUILayout.SelectionGrid((int)VisualsTab.SelectedObject, Main.buttons2.ToArray(), 1, Array.Empty<GUILayoutOption>());
			GUILayout.EndArea();
			Rect rect = new Rect(280f, 35f, 260f, 400f);
			GUIStyle guistyle = "box";
			GUILayout.BeginArea(rect, Enum.GetName(typeof(ESPObject), VisualsTab.SelectedObject), guistyle);
			VisualsTab.scrollPosition = GUILayout.BeginScrollView(VisualsTab.scrollPosition, Array.Empty<GUILayoutOption>());
			switch (VisualsTab.SelectedObject)
			{
			case ESPObject.Player:
				VisualsTab.SelectedOptions = G.Settings.PlayerOptions;
				VisualsTab.DrawGlobals(G.Settings.PlayerOptions, "Players");
				G.Settings.GlobalOptions.Weapon = GUILayout.Toggle(G.Settings.GlobalOptions.Weapon, "Show Weapon", Array.Empty<GUILayoutOption>());
				G.Settings.GlobalOptions.ViewHitboxes = GUILayout.Toggle(G.Settings.GlobalOptions.ViewHitboxes, "Show Expanded Hitboxes", Array.Empty<GUILayoutOption>());
				VisualsTab.DrawGlobals2(G.Settings.PlayerOptions);
				break;
			case ESPObject.Vehicle:
				VisualsTab.SelectedOptions = G.Settings.VehicleOptions;
				VisualsTab.DrawGlobals(G.Settings.VehicleOptions, "Vehicles");
				G.Settings.GlobalOptions.VehicleLocked = GUILayout.Toggle(G.Settings.GlobalOptions.VehicleLocked, "Show Lock State", Array.Empty<GUILayoutOption>());
				G.Settings.GlobalOptions.OnlyUnlocked = GUILayout.Toggle(G.Settings.GlobalOptions.OnlyUnlocked, "Only Display Unlocked Vehicles", Array.Empty<GUILayoutOption>());
				VisualsTab.DrawGlobals2(G.Settings.VehicleOptions);
				break;
			case ESPObject.Item:
				VisualsTab.DrawGlobals(G.Settings.ItemOptions, "Items");
				VisualsTab.SelectedOptions = G.Settings.ItemOptions;
				G.Settings.GlobalOptions.ListClumpedItems = GUILayout.Toggle(G.Settings.GlobalOptions.ListClumpedItems, "List Clumped Items", Array.Empty<GUILayoutOption>());
				if (G.Settings.GlobalOptions.ListClumpedItems)
				{
					GUILayout.Label("Clump Item Distance Minimum: " + Math.Round((double)G.Settings.GlobalOptions.DistanceThreshold, 1).ToString() + "m", Array.Empty<GUILayoutOption>());
					G.Settings.GlobalOptions.DistanceThreshold = GUILayout.HorizontalSlider(G.Settings.GlobalOptions.DistanceThreshold, 0.1f, 15f, Array.Empty<GUILayoutOption>());
					GUILayout.Label("Item Count Minimum: " + G.Settings.GlobalOptions.CountThreshold.ToString(), Array.Empty<GUILayoutOption>());
					G.Settings.GlobalOptions.CountThreshold = (int)GUILayout.HorizontalSlider((float)G.Settings.GlobalOptions.CountThreshold, 2f, 10f, Array.Empty<GUILayoutOption>());
				}
				if (GUILayout.Button("Open Whitelist Menu", Array.Empty<GUILayoutOption>()))
				{
					Items.editingaip = false;
					WhitelistWindow.WhitelistMenuOpen = true;
				}
				VisualsTab.DrawGlobals2(G.Settings.ItemOptions);
				break;
			case ESPObject.Zombie:
				VisualsTab.SelectedOptions = G.Settings.ZombieOptions;
				VisualsTab.DrawGlobals(G.Settings.ZombieOptions, "Zombies");
				VisualsTab.DrawGlobals2(G.Settings.ZombieOptions);
				break;
			case ESPObject.Ladder:
				VisualsTab.SelectedOptions = G.Settings.StorageOptions;
				VisualsTab.DrawGlobals(G.Settings.StorageOptions, "Storages");
				G.Settings.GlobalOptions.ShowLocked = GUILayout.Toggle(G.Settings.GlobalOptions.ShowLocked, "Show Lock State", Array.Empty<GUILayoutOption>());
				VisualsTab.DrawGlobals2(G.Settings.StorageOptions);
				break;
			case ESPObject.Storage:
				VisualsTab.SelectedOptions = G.Settings.BedOptions;
				VisualsTab.DrawGlobals(G.Settings.BedOptions, "Beds");
				G.Settings.GlobalOptions.Claimed = GUILayout.Toggle(G.Settings.GlobalOptions.Claimed, "Show Claimed State", Array.Empty<GUILayoutOption>());
				G.Settings.GlobalOptions.OnlyUnclaimed = GUILayout.Toggle(G.Settings.GlobalOptions.OnlyUnclaimed, "Only Display Unclaimed Beds", Array.Empty<GUILayoutOption>());
				VisualsTab.DrawGlobals2(G.Settings.BedOptions);
				break;
			case ESPObject.Bed:
				VisualsTab.SelectedOptions = G.Settings.FlagOptions;
				VisualsTab.DrawGlobals(G.Settings.FlagOptions, "Claim Flags");
				VisualsTab.DrawGlobals2(G.Settings.FlagOptions);
				break;
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		private static void DrawGlobals(ESPOptions options, string objname)
		{
			GUILayout.Space(2f);
			options.Enabled = GUILayout.Toggle(options.Enabled, objname + " - Enabled", Array.Empty<GUILayoutOption>());
			options.Box = GUILayout.Toggle(options.Box, "Box", Array.Empty<GUILayoutOption>());
			options.Glow = GUILayout.Toggle(options.Glow, "Glow", Array.Empty<GUILayoutOption>());
			options.Tracers = GUILayout.Toggle(options.Tracers, "Snaplines", Array.Empty<GUILayoutOption>());
			options.Name = GUILayout.Toggle(options.Name, "Name", Array.Empty<GUILayoutOption>());
			options.Distance = GUILayout.Toggle(options.Distance, "Distance", Array.Empty<GUILayoutOption>());
		}

		private static void DrawGlobals2(ESPOptions options)
		{
			if (GUILayout.Button("Cham Type: " + Enum.GetName(typeof(ShaderType), options.ChamType), Array.Empty<GUILayoutOption>()))
			{
				options.ChamType = options.ChamType.Next<ShaderType>();
			}
			GUILayout.Space(2f);
			GUILayout.Label("Max Render Distance: " + options.MaxDistance.ToString(), Array.Empty<GUILayoutOption>());
			options.MaxDistance = (int)GUILayout.HorizontalSlider((float)options.MaxDistance, 0f, 3000f, Array.Empty<GUILayoutOption>());
			GUILayout.Space(2f);
			GUILayout.Label("Font Size: " + options.FontSize.ToString(), Array.Empty<GUILayoutOption>());
			options.FontSize = (int)GUILayout.HorizontalSlider((float)options.FontSize, 0f, 24f, Array.Empty<GUILayoutOption>());
		}

		public static ESPObject SelectedObject = ESPObject.Player;

		private static Vector2 scrollPosition;

		public static ESPOptions SelectedOptions = G.Settings.PlayerOptions;
	}
}
