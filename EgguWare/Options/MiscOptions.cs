using System;
using EgguWare.Classes;

namespace EgguWare.Options
{
	// Token: 0x02000026 RID: 38
	public class MiscOptions
	{
		// Token: 0x04000068 RID: 104
		public bool FreeCam;

		// Token: 0x04000069 RID: 105
		public bool FullBright;

		// Token: 0x0400006A RID: 106
		public bool VehicleNoClip;

		// Token: 0x0400006B RID: 107
		public bool Spam;

		// Token: 0x0400006C RID: 108
		public string SpamText = "ballsack xD lol";

		// Token: 0x0400006D RID: 109
		public bool ShowEgguwareUser = true;

		// Token: 0x0400006E RID: 110
		public bool AutoItemPickup;

		// Token: 0x0400006F RID: 111
		public string UISkin = "";

		// Token: 0x04000070 RID: 112
		public bool ShowVanishPlayers;

		// Token: 0x04000071 RID: 113
		public ItemWhitelistObject AIPWhitelist = new ItemWhitelistObject();

		// Token: 0x04000072 RID: 114
		public ItemWhitelistObject ESPWhitelist = new ItemWhitelistObject();

		// Token: 0x04000073 RID: 115
		public bool PlayerFlight;

		// Token: 0x04000074 RID: 116
		public float PlayerFlightSpeedMult = 1f;

		// Token: 0x04000075 RID: 117
		public float RunspeedMult = 5f;

		// Token: 0x04000076 RID: 118
		public float JumpMult = 10f;

		// Token: 0x04000077 RID: 119
		public bool LimitFOV = true;

		// Token: 0x04000078 RID: 120
		public int ItemGrabFOV = 50;

		// Token: 0x04000079 RID: 121
		public bool AllOnMap = true;

		// Token: 0x0400007A RID: 122
		public bool DrawFOVCircle = true;

		// Token: 0x0400007B RID: 123
		public bool GrabItemThroughWalls = true;
	}
}
