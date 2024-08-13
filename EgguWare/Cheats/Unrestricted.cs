using System;
using System.Reflection;
using EgguWare.Attributes;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	// Token: 0x02000048 RID: 72
	[Comp]
	public class Unrestricted : MonoBehaviour
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000265C File Offset: 0x0000085C
		private void Update()
		{
			if (!Provider.isConnected)
			{
				G.UnrestrictedMovement = false;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000AB2C File Offset: 0x00008D2C
		private void FixedUpdate()
		{
			Unrestricted.PlayerFlight();
			if (G.UnrestrictedMovement)
			{
				this.standSpeed.SetValue(this.sprintSpeed, G.Settings.MiscOptions.RunspeedMult);
				this.proneSpeed.SetValue(this.sprintSpeed, G.Settings.MiscOptions.RunspeedMult);
				this.sprintSpeed.SetValue(this.sprintSpeed, G.Settings.MiscOptions.RunspeedMult);
				this.jumpHeight.SetValue(this.jumpHeight, G.Settings.MiscOptions.JumpMult);
				return;
			}
			this.standSpeed.SetValue(this.sprintSpeed, Unrestricted.SPEED_STAND);
			this.proneSpeed.SetValue(this.sprintSpeed, Unrestricted.SPEED_PRONE);
			this.sprintSpeed.SetValue(this.sprintSpeed, Unrestricted.SPEED_SPRINT);
			this.jumpHeight.SetValue(this.jumpHeight, Unrestricted.JUMP);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public static void PlayerFlight()
		{
			if (Player.player == null)
			{
				return;
			}
			Player player = Player.player;
			if (!G.Settings.MiscOptions.PlayerFlight)
			{
				ItemCloudAsset itemCloudAsset = player.equipment.asset as ItemCloudAsset;
				player.movement.itemGravityMultiplier = ((itemCloudAsset != null) ? itemCloudAsset.gravity : 1f);
				return;
			}
			player.movement.itemGravityMultiplier = 0f;
			float playerFlightSpeedMult = G.Settings.MiscOptions.PlayerFlightSpeedMult;
			if (Input.GetKey(KeyCode.Space))
			{
				player.transform.position += player.transform.up / 5f * playerFlightSpeedMult;
			}
			if (Input.GetKey(KeyCode.LeftControl))
			{
				player.transform.position -= player.transform.up / 5f * playerFlightSpeedMult;
			}
			if (Input.GetKey(KeyCode.W))
			{
				player.transform.position += player.transform.forward / 5f * playerFlightSpeedMult;
			}
			if (Input.GetKey(KeyCode.S))
			{
				player.transform.position -= player.transform.forward / 5f * playerFlightSpeedMult;
			}
			if (Input.GetKey(KeyCode.A))
			{
				player.transform.position -= player.transform.right / 5f * playerFlightSpeedMult;
			}
			if (Input.GetKey(KeyCode.D))
			{
				player.transform.position += player.transform.right / 5f * playerFlightSpeedMult;
			}
		}

		// Token: 0x0400012F RID: 303
		private FieldInfo standSpeed = typeof(PlayerMovement).GetField("SPEED_STAND", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000130 RID: 304
		private FieldInfo sprintSpeed = typeof(PlayerMovement).GetField("SPEED_SPRINT", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000131 RID: 305
		private FieldInfo proneSpeed = typeof(PlayerMovement).GetField("SPEED_PRONE", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000132 RID: 306
		private FieldInfo jumpHeight = typeof(PlayerMovement).GetField("JUMP", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000133 RID: 307
		private static readonly float SPEED_STAND = 4.5f;

		// Token: 0x04000134 RID: 308
		private static readonly float SPEED_PRONE = 1.5f;

		// Token: 0x04000135 RID: 309
		private static readonly float SPEED_SPRINT = 7f;

		// Token: 0x04000136 RID: 310
		private static readonly float JUMP = 7f;
	}
}
