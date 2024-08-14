using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace EgguWare.Utilities
{
	public class AssetUtilities
	{
		public static void GetAssets()
		{
			if (!Directory.Exists(Application.dataPath + "/GUISkins/"))
			{
				Directory.CreateDirectory(Application.dataPath + "/GUISkins/");
			}
			string text;
			if (File.Exists("/mnt/EgguWareV1.assets"))
			{
				text = "/mnt/EgguWareV1.assets";
			}
			else
			{
				if (!File.Exists("C/Program Files (x86)/steam/steamapps/common/Unturned/Unturned_Data/ManagedEgguWareV1.assets"))
				{
					Debug.LogError("File not found in any of the specified directories.");
					return;
				}
				text = "C/Program Files (x86)/steam/steamapps/common/Unturned/Unturned_Data/ManagedEgguWareV1.assets";
			}
			AssetBundle assetBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(text));
			foreach (Shader shader in assetBundle.LoadAllAssets<Shader>())
			{
				AssetUtilities.Shaders.Add(shader.name, shader);
			}
			AssetUtilities.BonkClip = assetBundle.LoadAllAssets<AudioClip>().First<AudioClip>();
			AssetUtilities.VanillaSkin = assetBundle.LoadAllAssets<GUISkin>().First<GUISkin>();
			AssetUtilities.VanillaSkin.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.label.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.button.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.box.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.window.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalSliderThumb.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalSlider.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalScrollbarUpButton.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalScrollbarThumb.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalScrollbarDownButton.font = AssetUtilities.dynamicFont;
			AssetUtilities.VanillaSkin.verticalScrollbar.font = AssetUtilities.dynamicFont;
			if (!string.IsNullOrEmpty(G.Settings.MiscOptions.UISkin))
			{
				AssetUtilities.LoadGUISkinFromName(G.Settings.MiscOptions.UISkin);
				return;
			}
			AssetUtilities.Skin = AssetUtilities.VanillaSkin;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000028BC File Offset: 0x00000ABC
		public static void LoadGUISkinFromName(string name)
		{
			if (File.Exists(Application.dataPath + "/GUISkins/" + name + ".assets"))
			{
				AssetBundle assetBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(Application.dataPath + "/GUISkins/" + name + ".assets"));
				AssetUtilities.Skin = assetBundle.LoadAllAssets<GUISkin>().First<GUISkin>();
				assetBundle.Unload(false);
				G.Settings.MiscOptions.UISkin = name;
				return;
			}
			G.Settings.MiscOptions.UISkin = "";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002940 File Offset: 0x00000B40
		public static List<string> GetSkins(bool Extensions = false)
		{
			List<string> list = new List<string>();
			foreach (FileInfo fileInfo in new DirectoryInfo(Application.dataPath + "/GUISkins/").GetFiles("*.assets"))
			{
				if (Extensions)
				{
					list.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length));
				}
				else
				{
					list.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length - 7));
				}
			}
			return list;
		}

		// Token: 0x04000006 RID: 6
		public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

		// Token: 0x04000007 RID: 7
		public static GUISkin Skin;

		// Token: 0x04000008 RID: 8
		public static GUISkin VanillaSkin;

		// Token: 0x04000009 RID: 9
		public static AudioClip BonkClip;

		// Token: 0x0400000A RID: 10
		public static Font dynamicFont = Font.CreateDynamicFontFromOSFont("Tahoma", 14);
	}
}
