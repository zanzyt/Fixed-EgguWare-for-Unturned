using System;
using System.Collections.Generic;
using System.IO;
using EgguWare.Options;
using Newtonsoft.Json;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{
	public class ConfigUtilities
	{
		private static string GetConfigPath(string name = "default")
		{
			return ConfigUtilities.ConfigPath + name + ".config";
		}

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

		public static string SelectedConfig = "default";

		private static string ConfigPath = Application.dataPath + "/EConfigs/";
	}
}
