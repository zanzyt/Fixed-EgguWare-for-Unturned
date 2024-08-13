using System;
using System.Collections.Generic;
using System.IO;
using EgguWare.Options;
using Newtonsoft.Json;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{
	// Token: 0x02000007 RID: 7
	public class ConfigUtilities
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002130 File Offset: 0x00000330
		private static string GetConfigPath(string name = "default")
		{
			return ConfigUtilities.ConfigPath + name + ".config";
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002142 File Offset: 0x00000342
		public static void CreateEnvironment()
		{
			if (!Directory.Exists(ConfigUtilities.ConfigPath))
			{
				Directory.CreateDirectory(ConfigUtilities.ConfigPath);
			}
			if (!File.Exists(ConfigUtilities.GetConfigPath("default")))
			{
				ConfigUtilities.SaveConfig("default", false);
				return;
			}
			ConfigUtilities.LoadConfig("default");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public static void SaveConfig(string name = "default", bool setconfig = false)
		{
			string configPath = ConfigUtilities.GetConfigPath(name);
			string text = JsonConvert.SerializeObject(G.Settings, Formatting.Indented);
			File.WriteAllText(configPath, text);
			if (setconfig)
			{
				ConfigUtilities.SelectedConfig = name;
			}
			if (Player.player && Provider.isConnected)
			{
				Player.player.StartCoroutine(T.Notify("Saved Config: " + name, Color.green, 3f));
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C60 File Offset: 0x00000E60
		public static void LoadConfig(string name = "default")
		{
			G.Settings = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigUtilities.GetConfigPath(name)));
			ConfigUtilities.SelectedConfig = name;
			if (Player.player && Provider.isConnected)
			{
				Player.player.StartCoroutine(T.Notify("Loaded Config: " + name, Color.blue, 3f));
			}
			Colors.AddColors();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public static List<string> GetConfigs(bool Extensions = false)
		{
			List<string> list = new List<string>();
			foreach (FileInfo fileInfo in new DirectoryInfo(ConfigUtilities.ConfigPath).GetFiles("*.config"))
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

		// Token: 0x0400000B RID: 11
		public static string SelectedConfig = "default";

		// Token: 0x0400000C RID: 12
		private static string ConfigPath = Application.dataPath + "/EConfigs/";
	}
}
