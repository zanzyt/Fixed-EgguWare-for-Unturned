using System;
using System.Collections.Generic;
using UnityEngine;

namespace EgguWare.Options.ESP
{
	// Token: 0x02000029 RID: 41
	public class OtherOptions
	{
		// Token: 0x04000093 RID: 147
		public bool Claimed = true;

		// Token: 0x04000094 RID: 148
		public bool OnlyUnclaimed;

		// Token: 0x04000095 RID: 149
		public bool ListClumpedItems;

		// Token: 0x04000096 RID: 150
		public float DistanceThreshold = 3f;

		// Token: 0x04000097 RID: 151
		public int CountThreshold = 5;

		// Token: 0x04000098 RID: 152
		public bool Weapon = true;

		// Token: 0x04000099 RID: 153
		public bool ViewHitboxes = true;

		// Token: 0x0400009A RID: 154
		public bool VehicleLocked = true;

		// Token: 0x0400009B RID: 155
		public bool OnlyUnlocked;

		// Token: 0x0400009C RID: 156
		public bool ShowLocked = true;

		// Token: 0x0400009D RID: 157
		public Dictionary<string, Color32> GlobalColors = new Dictionary<string, Color32>();
	}
}
