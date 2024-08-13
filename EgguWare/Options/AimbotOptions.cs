using System;
using System.Collections.Generic;
using EgguWare.Classes;
using UnityEngine;

namespace EgguWare.Options
{
	// Token: 0x02000024 RID: 36
	public class AimbotOptions
	{
		// Token: 0x04000046 RID: 70
		public bool SilentAim;

		// Token: 0x04000047 RID: 71
		public bool SilentAimInfo = true;

		// Token: 0x04000048 RID: 72
		public int AimpointMultiplier = 1;

		// Token: 0x04000049 RID: 73
		public int HitboxSize = 15;

		// Token: 0x0400004A RID: 74
		public TargetLimb1 TargetL = TargetLimb1.SKULL;

		// Token: 0x0400004B RID: 75
		public KeyCode AimlockKey = KeyCode.F;

		// Token: 0x0400004C RID: 76
		public List<ESPObject> SilentAimObjects = new List<ESPObject>();

		// Token: 0x0400004D RID: 77
		public bool OnlyVisible;

		// Token: 0x0400004E RID: 78
		public bool Aimlock;

		// Token: 0x0400004F RID: 79
		public bool Mouse1Aimbot;

		// Token: 0x04000050 RID: 80
		public bool AimlockLimitFOV;

		// Token: 0x04000051 RID: 81
		public int AimlockFOV = 200;

		// Token: 0x04000052 RID: 82
		public bool AimlockDrawFOV = true;

		// Token: 0x04000053 RID: 83
		public bool SilentAimLimitFOV;

		// Token: 0x04000054 RID: 84
		public int SilentAimFOV = 200;

		// Token: 0x04000055 RID: 85
		public bool SilentAimDrawFOV = true;

		// Token: 0x04000056 RID: 86
		public bool ExpandHitboxes = true;

		// Token: 0x04000057 RID: 87
		public int HitChance = 100;

		// Token: 0x04000058 RID: 88
		public bool rocketRape;

		// Token: 0x04000059 RID: 89
		public bool Aimlockinfo = true;
	}
}
