using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EgguWare.Utilities
{
	public class OverrideHelper
	{
		[DllImport("mono.dll", CallingConvention = CallingConvention.FastCall)]
		private static extern IntPtr mono_domain_get();

		[DllImport("mono.dll", CallingConvention = CallingConvention.FastCall)]
		private static extern IntPtr mono_method_get_header(IntPtr method);

		public static void RedirectCalls(MethodInfo from, MethodInfo to)
		{
			IntPtr functionPointer = from.MethodHandle.GetFunctionPointer();
			IntPtr functionPointer2 = to.MethodHandle.GetFunctionPointer();
			OverrideHelper.PatchJumpTo(functionPointer, functionPointer2);
		}

		private unsafe static void RedirectCall(MethodInfo from, MethodInfo to)
		{
			IntPtr value = from.MethodHandle.Value;
			IntPtr value2 = to.MethodHandle.Value;
			from.MethodHandle.GetFunctionPointer();
			to.MethodHandle.GetFunctionPointer();
			byte* ptr = (byte*)OverrideHelper.mono_domain_get().ToPointer() + 232;
			long** ptr2 = *(IntPtr*)((byte*)ptr + 32);
			uint num = *(uint*)((byte*)ptr + 24);
			void* ptr3 = null;
			void* ptr4 = null;
			long num2 = value.ToInt64();
			uint num3 = (uint)num2 >> 3;
			for (long* ptr5 = *(IntPtr*)(ptr2 + (ulong)(num3 % num) * (ulong)((long)sizeof(long*)) / (ulong)sizeof(long*)); ptr5 != null; ptr5 = *(IntPtr*)(ptr5 + 1))
			{
				if (num2 == *ptr5)
				{
					ptr3 = (void*)ptr5;
					break;
				}
			}
			long num4 = value2.ToInt64();
			uint num5 = (uint)num4 >> 3;
			for (long* ptr6 = *(IntPtr*)(ptr2 + (ulong)(num5 % num) * (ulong)((long)sizeof(long*)) / (ulong)sizeof(long*)); ptr6 != null; ptr6 = *(IntPtr*)(ptr6 + 1))
			{
				if (num4 == *ptr6)
				{
					ptr4 = (void*)ptr6;
					break;
				}
			}
			if (ptr3 == null || ptr4 == null)
			{
				Debug.Log("Could not find methods");
				return;
			}
			ulong* ptr7 = (ulong*)ptr3;
			ulong* ptr8 = (ulong*)ptr4;
			ptr7[2] = ptr8[2];
			ptr7[3] = ptr8[3];
		}

		private unsafe static void PatchJumpTo(IntPtr site, IntPtr target)
		{
			byte* ptr = (byte*)site.ToPointer();
			*ptr = 73;
			ptr[1] = 187;
			*(long*)(ptr + 2) = target.ToInt64();
			ptr[10] = 65;
			ptr[11] = byte.MaxValue;
			ptr[12] = 227;
		}
		
		private unsafe static void RedirectCallIL(MethodInfo from, MethodInfo to)
		{
			IntPtr value = from.MethodHandle.Value;
			IntPtr value2 = to.MethodHandle.Value;
			OverrideHelper.mono_method_get_header(value2);
			byte* ptr = (byte*)value.ToPointer();
			byte* ptr2 = (byte*)value2.ToPointer();
			*(IntPtr*)(ptr + 40) = *(IntPtr*)(ptr2 + 40);
		}
	}
}
