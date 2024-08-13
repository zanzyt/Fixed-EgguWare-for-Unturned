using System;
using System.Collections.Generic;
using System.Reflection;
using EgguWare.Utilities;
using SDG.Unturned;

namespace EgguWare.Overrides
{
	// Token: 0x0200001B RID: 27
	public class hkLocalHwid
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000060D8 File Offset: 0x000042D8
		public static byte[] OV_getHwid()
		{
			List<byte> list = new List<byte>();
			for (int i = 0; i < 20; i++)
			{
				list.Add((byte)T.Random.Next(0, 100));
			}
			typeof(LocalHwid).GetField("cachedHwid", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, list.ToArray());
			return list.ToArray();
		}
	}
}
