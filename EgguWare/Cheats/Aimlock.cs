using System;
using EgguWare.Attributes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	// Token: 0x02000042 RID: 66
	[Comp]
	public class Aimlock : MonoBehaviour
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00009030 File Offset: 0x00007230
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

		// Token: 0x060000CD RID: 205 RVA: 0x000090F0 File Offset: 0x000072F0
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

		// Token: 0x0400011F RID: 287
		public static bool Aiming;
	}
}
