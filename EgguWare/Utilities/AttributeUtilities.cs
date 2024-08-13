using System;
using System.Reflection;
using EgguWare.Attributes;

namespace EgguWare.Utilities
{
	// Token: 0x02000005 RID: 5
	public static class AttributeUtilities
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static void LinkAttributes()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.IsDefined(typeof(Comp), false))
				{
					Load.CO.AddComponent(type);
				}
			}
		}
	}
}
