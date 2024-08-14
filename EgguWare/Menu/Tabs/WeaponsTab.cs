using System;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{

	public class WeaponsTab
	{

		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 260f, 400f), "Modifiers", "box");
			G.Settings.WeaponOptions.NoSpread = GUILayout.Toggle(G.Settings.WeaponOptions.NoSpread, "Remove Spread", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.NoRecoil = GUILayout.Toggle(G.Settings.WeaponOptions.NoRecoil, "Remove Recoil", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.NoSway = GUILayout.Toggle(G.Settings.WeaponOptions.NoSway, "Remove Sway", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.RemoveBurstDelay = GUILayout.Toggle(G.Settings.WeaponOptions.RemoveBurstDelay, "Remove Burst Delay", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.RemoveHammerDelay = GUILayout.Toggle(G.Settings.WeaponOptions.RemoveHammerDelay, "Remove Hammer Delay", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.InstantReload = GUILayout.Toggle(G.Settings.WeaponOptions.InstantReload, "Remove Reload Delay", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.FastGun = GUILayout.Toggle(G.Settings.WeaponOptions.FastGun, "Fast Gun", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.NoSlow = GUILayout.Toggle(G.Settings.WeaponOptions.NoSlow, "No Slow", Array.Empty<GUILayoutOption>());
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(280f, 35f, 260f, 400f), "Other", "box");
			G.Settings.WeaponOptions.WeaponInfo = GUILayout.Toggle(G.Settings.WeaponOptions.WeaponInfo, "Show Weapon Info", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.DamageIndicators = GUILayout.Toggle(G.Settings.WeaponOptions.DamageIndicators, "Damage Indicators", Array.Empty<GUILayoutOption>());
			if (G.Settings.WeaponOptions.DamageIndicators)
			{
				G.Settings.WeaponOptions.DamageIndicatorDamageScaling = GUILayout.Toggle(G.Settings.WeaponOptions.DamageIndicatorDamageScaling, "Scale Color By Damage", Array.Empty<GUILayoutOption>());
			}
			G.Settings.WeaponOptions.HitmarkerBonk = GUILayout.Toggle(G.Settings.WeaponOptions.HitmarkerBonk, "Hitmarker Bonk™\ufe0f", Array.Empty<GUILayoutOption>());
			G.Settings.WeaponOptions.TracerLines = GUILayout.Toggle(G.Settings.WeaponOptions.TracerLines, "Bullet Tracers", Array.Empty<GUILayoutOption>());
			if (G.Settings.WeaponOptions.TracerLines)
			{
				GUILayout.Label("Tracer Expire Time: " + G.Settings.WeaponOptions.TracerTime.ToString(), Array.Empty<GUILayoutOption>());
				G.Settings.WeaponOptions.TracerTime = (int)GUILayout.HorizontalSlider((float)G.Settings.WeaponOptions.TracerTime, 1f, 10f, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
		}
	}
}
