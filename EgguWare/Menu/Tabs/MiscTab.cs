using System;
using EgguWare.Cheats;
using EgguWare.Classes;
using EgguWare.Menu.Windows;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{
	public class MiscTab
	{
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 260f, 400f), "Game", "box");
			G.Settings.MiscOptions.FreeCam = GUILayout.Toggle(G.Settings.MiscOptions.FreeCam, "Free Cam", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.VehicleNoClip = GUILayout.Toggle(G.Settings.MiscOptions.VehicleNoClip, "Vehicle No-Clip", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.ShowVanishPlayers = GUILayout.Toggle(G.Settings.MiscOptions.ShowVanishPlayers, "Show Vanished Players", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.FullBright = GUILayout.Toggle(G.Settings.MiscOptions.FullBright, "Full Bright (Does not revert on spy)", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.AllOnMap = GUILayout.Toggle(G.Settings.MiscOptions.AllOnMap, "Show Players On Map", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.SpamText = GUILayout.TextField(G.Settings.MiscOptions.SpamText, Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.Spam = GUILayout.Toggle(G.Settings.MiscOptions.Spam, "Enable Spam", Array.Empty<GUILayoutOption>());
			G.Settings.MiscOptions.GrabItemThroughWalls = GUILayout.Toggle(G.Settings.MiscOptions.GrabItemThroughWalls, "Take Through Walls", Array.Empty<GUILayoutOption>());
			if (G.Settings.MiscOptions.GrabItemThroughWalls)
			{
				G.Settings.MiscOptions.LimitFOV = GUILayout.Toggle(G.Settings.MiscOptions.LimitFOV, "Pixel FOV Limit", Array.Empty<GUILayoutOption>());
				if (G.Settings.MiscOptions.LimitFOV)
				{
					GUILayout.Label("Pixels: " + G.Settings.MiscOptions.ItemGrabFOV.ToString(), Array.Empty<GUILayoutOption>());
					G.Settings.MiscOptions.ItemGrabFOV = (int)GUILayout.HorizontalSlider((float)G.Settings.MiscOptions.ItemGrabFOV, 1f, 1200f, Array.Empty<GUILayoutOption>());
					G.Settings.MiscOptions.DrawFOVCircle = GUILayout.Toggle(G.Settings.MiscOptions.DrawFOVCircle, "Draw Pixel FOV Circle", Array.Empty<GUILayoutOption>());
				}
			}
			G.Settings.MiscOptions.AutoItemPickup = GUILayout.Toggle(G.Settings.MiscOptions.AutoItemPickup, "Auto Item Pickup", Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("Open Whitelist Menu", Array.Empty<GUILayoutOption>()))
			{
				EgguWare.Cheats.Items.editingaip = true;
				WhitelistWindow.WhitelistMenuOpen = true;
			}
			if (GUILayout.Button("GUI Skin Changer", Array.Empty<GUILayoutOption>()))
			{
				GUIWindow.GUISkinMenuOpen = !GUIWindow.GUISkinMenuOpen;
			}
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(280f, 35f, 260f, 400f), "Movement", "box");
			if (!G.UnrestrictedMovement)
			{
				if (GUILayout.Button("Check Movement Verification", Array.Empty<GUILayoutOption>()))
				{
					Misc.instance.StartCoroutine(T.CheckVerification(Player.player.transform.position));
				}
			}
			else
			{
				if (GUILayout.Button("Disable Movement Modifiers", Array.Empty<GUILayoutOption>()))
				{
					G.UnrestrictedMovement = false;
				}
				G.Settings.MiscOptions.PlayerFlight = GUILayout.Toggle(G.Settings.MiscOptions.PlayerFlight, "Player No-Clip", Array.Empty<GUILayoutOption>());
				if (G.Settings.MiscOptions.PlayerFlight)
				{
					GUILayout.Label("Player Flight Multiplier: " + G.Settings.MiscOptions.PlayerFlightSpeedMult.ToString() + "x", Array.Empty<GUILayoutOption>());
					G.Settings.MiscOptions.PlayerFlightSpeedMult = GUILayout.HorizontalSlider(G.Settings.MiscOptions.PlayerFlightSpeedMult, 0.1f, 100f, Array.Empty<GUILayoutOption>());
				}
				GUILayout.Space(5f);
				GUILayout.Label("Walk Speed: " + G.Settings.MiscOptions.RunspeedMult.ToString(), Array.Empty<GUILayoutOption>());
				G.Settings.MiscOptions.RunspeedMult = GUILayout.HorizontalSlider(G.Settings.MiscOptions.RunspeedMult, 1f, 500f, Array.Empty<GUILayoutOption>());
				GUILayout.Space(5f);
				GUILayout.Label("Jump Modifier: " + G.Settings.MiscOptions.JumpMult.ToString(), Array.Empty<GUILayoutOption>());
				G.Settings.MiscOptions.JumpMult = GUILayout.HorizontalSlider(G.Settings.MiscOptions.JumpMult, 1f, 150f, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
		}

		public static MiscOptions SelectedObject;
	}
}
