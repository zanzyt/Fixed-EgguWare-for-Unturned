using System;
using EgguWare.Classes;
using EgguWare.Utilities;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{
	public class AimbotTab
	{
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 260f, 400f), "Silent Aimbot", "box");
			AimbotTab.scrollPosition = GUILayout.BeginScrollView(AimbotTab.scrollPosition, Array.Empty<GUILayoutOption>());
			G.Settings.AimbotOptions.SilentAim = GUILayout.Toggle(G.Settings.AimbotOptions.SilentAim, "Silent Aim", Array.Empty<GUILayoutOption>());
			if (G.Settings.AimbotOptions.SilentAim)
			{
				G.Settings.AimbotOptions.ExpandHitboxes = GUILayout.Toggle(G.Settings.AimbotOptions.ExpandHitboxes, "Expand Hitboxes", Array.Empty<GUILayoutOption>());
				if (G.Settings.AimbotOptions.ExpandHitboxes)
				{
					GUILayout.Label("Aimpoint Multiplier: " + G.Settings.AimbotOptions.AimpointMultiplier.ToString(), Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.AimpointMultiplier = (int)GUILayout.HorizontalSlider((float)G.Settings.AimbotOptions.AimpointMultiplier, 1f, 3f, Array.Empty<GUILayoutOption>());
					GUILayout.Space(2f);
					GUILayout.Label("Hitbox Width: " + G.Settings.AimbotOptions.HitboxSize.ToString() + "m", Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.HitboxSize = (int)GUILayout.HorizontalSlider((float)G.Settings.AimbotOptions.HitboxSize, 1f, 15f, Array.Empty<GUILayoutOption>());
					GUILayout.Space(2f);
				}
				GUILayout.Label("Chance To Hit: " + G.Settings.AimbotOptions.HitChance.ToString() + "%", Array.Empty<GUILayoutOption>());
				G.Settings.AimbotOptions.HitChance = (int)GUILayout.HorizontalSlider((float)G.Settings.AimbotOptions.HitChance, 1f, 100f, Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				if (GUILayout.Button("Silent Aim Limb: " + Enum.GetName(typeof(TargetLimb1), G.Settings.AimbotOptions.TargetL), Array.Empty<GUILayoutOption>()))
				{
					G.Settings.AimbotOptions.TargetL = G.Settings.AimbotOptions.TargetL.Next<TargetLimb1>();
				}
				G.Settings.AimbotOptions.SilentAimLimitFOV = GUILayout.Toggle(G.Settings.AimbotOptions.SilentAimLimitFOV, "Pixel FOV Limit", Array.Empty<GUILayoutOption>());
				if (G.Settings.AimbotOptions.SilentAimLimitFOV)
				{
					GUILayout.Label("Pixels: " + G.Settings.AimbotOptions.SilentAimFOV.ToString(), Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.SilentAimFOV = (int)GUILayout.HorizontalSlider((float)G.Settings.AimbotOptions.SilentAimFOV, 1f, 1200f, Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.SilentAimDrawFOV = GUILayout.Toggle(G.Settings.AimbotOptions.SilentAimDrawFOV, "Draw Pixel FOV Circle", Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(280f, 35f, 260f, 400f), "Aimlock", "box");
			G.Settings.AimbotOptions.Aimlock = GUILayout.Toggle(G.Settings.AimbotOptions.Aimlock, "Aimlock", Array.Empty<GUILayoutOption>());
			if (G.Settings.AimbotOptions.Aimlock)
			{
				if (GUILayout.Button("Aimlock Key: " + G.Settings.AimbotOptions.AimlockKey.ToString(), Array.Empty<GUILayoutOption>()))
				{
					G.Settings.AimbotOptions.AimlockKey = KeyCode.None;
				}
				G.Settings.AimbotOptions.AimlockLimitFOV = GUILayout.Toggle(G.Settings.AimbotOptions.AimlockLimitFOV, "Pixel FOV Limit", Array.Empty<GUILayoutOption>());
				if (G.Settings.AimbotOptions.AimlockLimitFOV)
				{
					GUILayout.Label("Pixels: " + G.Settings.AimbotOptions.AimlockFOV.ToString(), Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.AimlockFOV = (int)GUILayout.HorizontalSlider((float)G.Settings.AimbotOptions.AimlockFOV, 1f, 1200f, Array.Empty<GUILayoutOption>());
					G.Settings.AimbotOptions.AimlockDrawFOV = GUILayout.Toggle(G.Settings.AimbotOptions.AimlockDrawFOV, "Draw Pixel FOV Circle", Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndArea();
			if (G.Settings.AimbotOptions.AimlockKey == KeyCode.None)
			{
				Event current = Event.current;
				G.Settings.AimbotOptions.AimlockKey = current.keyCode;
			}
		}

		public static AimbotOptions SelectedObject;

		private static Vector2 scrollPosition;
	}
}
