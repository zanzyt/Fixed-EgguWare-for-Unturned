using System;
using EgguWare.Attributes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class Aimlock : MonoBehaviour
	{
		private void Update()
		{
			if (G.Settings.AimbotOptions.Aimlock)
			{
				if (Input.GetKeyDown(G.Settings.AimbotOptions.AimlockKey))
				{
					Aimlock.Aiming = true;
				}
				if (Input.GetKeyUp(G.Settings.AimbotOptions.AimlockKey))
				{
					Aimlock.Aiming = false;
				}
				if (Aimlock.Aiming)
				{
					int? num = null;
					if (G.Settings.AimbotOptions.AimlockLimitFOV)
					{
						num = new int?(G.Settings.AimbotOptions.AimlockFOV);
					}
					Player nearestPlayer = T.GetNearestPlayer(num, null);
					if (nearestPlayer != null)
					{
						T.AimAt(T.GetLimbPosition(nearestPlayer.transform, "Skull"));
					}
				}
			}
		}

		private void OnGUI()
		{
			if (!G.BeingSpied && Provider.isConnected)
			{
				if (G.Settings.AimbotOptions.AimlockDrawFOV && G.Settings.AimbotOptions.AimlockLimitFOV && G.Settings.AimbotOptions.Aimlock)
				{
					T.DrawCircle(Colors.GetColor("Aimlock_FOV_Circle"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)G.Settings.AimbotOptions.AimlockFOV);
				}
				if (G.Settings.AimbotOptions.SilentAimDrawFOV && G.Settings.AimbotOptions.SilentAim && G.Settings.AimbotOptions.SilentAimLimitFOV)
				{
					T.DrawCircle(Colors.GetColor("Silent_Aim_FOV_Circle"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)G.Settings.AimbotOptions.SilentAimFOV);
				}
			}
		}

		public static bool Aiming;
	}
}
