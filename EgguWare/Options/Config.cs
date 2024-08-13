using System;
using System.Collections.Generic;
using EgguWare.Classes;
using EgguWare.Options.ESP;

namespace EgguWare.Options
{
	// Token: 0x02000025 RID: 37
	public class Config
	{
		// Token: 0x0400005A RID: 90
		public ESPOptions BedOptions = new ESPOptions();

		// Token: 0x0400005B RID: 91
		public ESPOptions PlayerOptions = new ESPOptions();

		// Token: 0x0400005C RID: 92
		public ESPOptions ItemOptions = new ESPOptions();

		// Token: 0x0400005D RID: 93
		public ESPOptions StorageOptions = new ESPOptions();

		// Token: 0x0400005E RID: 94
		public ESPOptions VehicleOptions = new ESPOptions();

		// Token: 0x0400005F RID: 95
		public ESPOptions ZombieOptions = new ESPOptions();

		// Token: 0x04000060 RID: 96
		public ESPOptions FlagOptions = new ESPOptions();

		// Token: 0x04000061 RID: 97
		public OtherOptions GlobalOptions = new OtherOptions();

		// Token: 0x04000062 RID: 98
		public AimbotOptions AimbotOptions = new AimbotOptions();

		// Token: 0x04000063 RID: 99
		public WeaponOptions WeaponOptions = new WeaponOptions();

		// Token: 0x04000064 RID: 100
		public MiscOptions MiscOptions = new MiscOptions();

		// Token: 0x04000065 RID: 101
		public Dictionary<ulong, Priority> Priority = new Dictionary<ulong, Priority>();

		// Token: 0x04000066 RID: 102
		public Dictionary<ulong, TargetLimb> TargetLimb = new Dictionary<ulong, TargetLimb>();

		// Token: 0x04000067 RID: 103
		public Dictionary<ulong, Mute> Mute = new Dictionary<ulong, Mute>();
	}
}
