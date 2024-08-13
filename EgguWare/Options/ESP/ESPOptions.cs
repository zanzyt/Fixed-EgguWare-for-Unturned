using System;
using EgguWare.Classes;

namespace EgguWare.Options.ESP
{
	// Token: 0x02000028 RID: 40
	public class ESPOptions
	{
		// Token: 0x0400008A RID: 138
		public bool Enabled;

		// Token: 0x0400008B RID: 139
		public bool Glow = true;

		// Token: 0x0400008C RID: 140
		public bool Box = true;

		// Token: 0x0400008D RID: 141
		public bool Distance = true;

		// Token: 0x0400008E RID: 142
		public bool Name = true;

		// Token: 0x0400008F RID: 143
		public bool Tracers = true;

		// Token: 0x04000090 RID: 144
		public int MaxDistance = 400;

		// Token: 0x04000091 RID: 145
		public int FontSize = 11;

		// Token: 0x04000092 RID: 146
		public ShaderType ChamType;
	}
}
