using System;
using System.Reflection;
using EgguWare.Attributes;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class Unrestricted : MonoBehaviour
	{

		private void Update()
		{
			if (!Provider.isConnected)
			{
				G.UnrestrictedMovement = false;
			}
		}

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

		private FieldInfo standSpeed = typeof(PlayerMovement).GetField("SPEED_STAND", BindingFlags.Static | BindingFlags.NonPublic);

		private FieldInfo sprintSpeed = typeof(PlayerMovement).GetField("SPEED_SPRINT", BindingFlags.Static | BindingFlags.NonPublic);

		private FieldInfo proneSpeed = typeof(PlayerMovement).GetField("SPEED_PRONE", BindingFlags.Static | BindingFlags.NonPublic);

		private FieldInfo jumpHeight = typeof(PlayerMovement).GetField("JUMP", BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly float SPEED_STAND = 4.5f;

		private static readonly float SPEED_PRONE = 1.5f;

		private static readonly float SPEED_SPRINT = 7f;

		private static readonly float JUMP = 7f;
	}
}
