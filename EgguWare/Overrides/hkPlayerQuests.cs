using System;
using SDG.Unturned;

namespace EgguWare.Overrides
{
	// Token: 0x02000020 RID: 32
	public class hkPlayerQuests
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00002426 File Offset: 0x00000626
		public bool OV_isMemberOfSameGroupAs(Player player)
		{
			return G.Settings.MiscOptions.AllOnMap && !G.BeingSpied;
		}
	}
}
