using System;
using System.Reflection;
using EgguWare.Attributes;

namespace EgguWare.Utilities
{
	public static class AttributeUtilities
	{
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
