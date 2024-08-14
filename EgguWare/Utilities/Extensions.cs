using System;

namespace EgguWare.Utilities
{
	public static class Extensions
	{
		public static T Next<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));
			}
			T[] array = (T[])Enum.GetValues(src.GetType());
			int num = Array.IndexOf<T>(array, src) + 1;
			if (array.Length != num)
			{
				return array[num];
			}
			return array[0];
		}
	}
}
