using System;
using System.Collections.Generic;
using System.Reflection;
using EgguWare.Utilities;
using SDG.Unturned;

namespace EgguWare.Overrides
{
	public class hkLocalHwid
	{
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
