using System;
using EgguWare.Classes;

namespace EgguWare.Options
{
	public class MiscOptions
	{
		public bool FreeCam;

		public bool FullBright;

		public bool VehicleNoClip;

		public bool Spam;

		public string SpamText = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

		public bool ShowEgguwareUser = true;

		public bool AutoItemPickup;

		public string UISkin = "";

		public bool ShowVanishPlayers;

		public ItemWhitelistObject AIPWhitelist = new ItemWhitelistObject();

		public ItemWhitelistObject ESPWhitelist = new ItemWhitelistObject();

		public bool PlayerFlight;

		public float PlayerFlightSpeedMult = 1f;

		public float RunspeedMult = 5f;

		public float JumpMult = 10f;

		public bool LimitFOV = true;

		public int ItemGrabFOV = 50;

		public bool AllOnMap = true;

		public bool DrawFOVCircle = true;

		public bool GrabItemThroughWalls = true;
	}
}
