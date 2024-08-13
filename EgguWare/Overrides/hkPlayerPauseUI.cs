using System;
using SDG.Unturned;

namespace EgguWare.Overrides
{
	// Token: 0x0200001F RID: 31
	public class hkPlayerPauseUI
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000241F File Offset: 0x0000061F
		public static void OV_onClickedExitButton(ISleekElement button)
		{
			Provider.disconnect();
		}
	}
}
