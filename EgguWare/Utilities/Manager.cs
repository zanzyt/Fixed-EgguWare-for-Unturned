using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EgguWare.Cheats;
using EgguWare.Classes;
using EgguWare.Overrides;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{
	public class Manager : MonoBehaviour
	{
		private void Start()
		{
			File.WriteAllText("llka.log", "");
			T.DrawMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			T.DrawMaterial.SetInt("_SrcBlend", 5);
			T.DrawMaterial.SetInt("_DstBlend", 10);
			T.DrawMaterial.SetInt("_Cull", 0);
			T.DrawMaterial.SetInt("_ZWrite", 0);
			T.Log("Loading llka");
			AttributeUtilities.LinkAttributes();
			T.Log("Adding Attributes");
			ConfigUtilities.CreateEnvironment();
			T.Log("Getting Config");
			AssetUtilities.GetAssets();
			T.Log("Getting Assets");
			Colors.AddColors();
			base.StartCoroutine(this.UpdateESPObjects());
			GraphicsSettings.outlineQuality = EGraphicQuality.ULTRA;
			T.Log("Starting Overrides");
			T.OverrideMethod(typeof(DamageTool), typeof(hkDamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public, new Type[]
			{
				typeof(Ray),
				typeof(float),
				typeof(int),
				typeof(Player)
			});
			T.OverrideMethod(typeof(PlayerPauseUI), typeof(hkPlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(Provider), typeof(hkProvider), "onApplicationWantsToQuit", BindingFlags.Instance | BindingFlags.NonPublic, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(Player), typeof(hkPlayer), "ReceiveTakeScreenshot", BindingFlags.Instance | BindingFlags.Public, BindingFlags.Instance | BindingFlags.Public);
			T.OverrideMethod(typeof(UseableGun), typeof(hkUsableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, BindingFlags.Instance | BindingFlags.Public);
			T.OverrideMethod(typeof(ChatManager), typeof(hkChatManager), "receiveChatMessage", BindingFlags.Static | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(LocalHwid), typeof(hkLocalHwid), "getHwid", BindingFlags.Static | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(ItemManager), typeof(hkItemManager), "getItemsInRadius", BindingFlags.Static | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(Regions), typeof(hkRegions), "getRegionsInRadius", BindingFlags.Static | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public);
			T.OverrideMethod(typeof(PlayerQuests), typeof(hkPlayerQuests), "isMemberOfSameGroupAs", BindingFlags.Instance | BindingFlags.Public, BindingFlags.Instance | BindingFlags.Public);
			T.Log("Overrides Complete");
		}

		private void OnGUI()
		{
			if (G.MainCamera == null)
			{
				G.MainCamera = Camera.main;
			}
		}

		private IEnumerator UpdateESPObjects()
		{
			for (;;)
			{
				if (Provider.isConnected && G.MainCamera != null)
				{
					List<SteamPlayer> list = new List<SteamPlayer>();
					List<ESPObj> list2 = new List<ESPObj>();
					if (G.Settings.ItemOptions.Enabled)
					{
						foreach (InteractableItem interactableItem in global::UnityEngine.Object.FindObjectsOfType<InteractableItem>())
						{
							if (T.IsItemWhitelisted(interactableItem, G.Settings.MiscOptions.ESPWhitelist))
							{
								ESPObj espobj = new ESPObj(ESPObject.Item, interactableItem, interactableItem.gameObject, G.Settings.ItemOptions);
								list2.Add(espobj);
								if (!G.BeingSpied)
								{
									ESP.ApplyChams(espobj, Colors.GetColor("Item_Chams_Visible_Color"), Colors.GetColor("Item_Chams_Occluded_Color"));
								}
							}
						}
					}
					if (G.Settings.FlagOptions.Enabled)
					{
						foreach (InteractableClaim interactableClaim in global::UnityEngine.Object.FindObjectsOfType<InteractableClaim>())
						{
							ESPObj espobj2 = new ESPObj(ESPObject.Bed, interactableClaim, interactableClaim.gameObject, G.Settings.FlagOptions);
							list2.Add(espobj2);
							if (!G.BeingSpied)
							{
								ESP.ApplyChams(espobj2, Colors.GetColor("Flag_Chams_Visible_Color"), Colors.GetColor("Flag_Chams_Occluded_Color"));
							}
						}
					}
					if (G.Settings.StorageOptions.Enabled)
					{
						foreach (InteractableStorage interactableStorage in global::UnityEngine.Object.FindObjectsOfType<InteractableStorage>())
						{
							ESPObj espobj3 = new ESPObj(ESPObject.Ladder, interactableStorage, interactableStorage.gameObject, G.Settings.StorageOptions);
							list2.Add(espobj3);
							if (!G.BeingSpied)
							{
								ESP.ApplyChams(espobj3, Colors.GetColor("Storage_Chams_Visible_Color"), Colors.GetColor("Storage_Chams_Occluded_Color"));
							}
						}
					}
					if (G.Settings.ZombieOptions.Enabled)
					{
						foreach (Zombie zombie in global::UnityEngine.Object.FindObjectsOfType<Zombie>())
						{
							ESPObj espobj4 = new ESPObj(ESPObject.Zombie, zombie, zombie.gameObject, G.Settings.ZombieOptions);
							list2.Add(espobj4);
							if (!G.BeingSpied)
							{
								ESP.ApplyChams(espobj4, Colors.GetColor("Zombie_Chams_Visible_Color"), Colors.GetColor("Zombie_Chams_Occluded_Color"));
							}
						}
					}
					if (G.Settings.BedOptions.Enabled)
					{
						foreach (InteractableBed interactableBed in global::UnityEngine.Object.FindObjectsOfType<InteractableBed>())
						{
							ESPObj espobj5 = new ESPObj(ESPObject.Storage, interactableBed, interactableBed.gameObject, G.Settings.BedOptions);
							list2.Add(espobj5);
							if (!G.BeingSpied)
							{
								ESP.ApplyChams(espobj5, Colors.GetColor("Bed_Chams_Visible_Color"), Colors.GetColor("Bed_Chams_Occluded_Color"));
							}
						}
					}
					if (G.Settings.VehicleOptions.Enabled)
					{
						foreach (InteractableVehicle interactableVehicle in global::UnityEngine.Object.FindObjectsOfType<InteractableVehicle>())
						{
							if (!G.Settings.GlobalOptions.OnlyUnlocked || !interactableVehicle.isLocked)
							{
								ESPObj espobj6 = new ESPObj(ESPObject.Vehicle, interactableVehicle, interactableVehicle.gameObject, G.Settings.VehicleOptions);
								list2.Add(espobj6);
								if (!G.BeingSpied)
								{
									ESP.ApplyChams(espobj6, Colors.GetColor("Vehicle_Chams_Visible_Color"), Colors.GetColor("Vehicle_Chams_Occluded_Color"));
								}
							}
						}
					}
					foreach (SteamPlayer steamPlayer in Provider.clients)
					{
						if (steamPlayer != Player.player.channel.owner)
						{
							ESPObj espobj7 = new ESPObj(ESPObject.Player, steamPlayer, steamPlayer.player.gameObject, G.Settings.PlayerOptions);
							list2.Add(espobj7);
							Color color = Colors.GetColor("Player_Chams_Occluded_Color");
							Color color2 = Colors.GetColor("Player_Chams_Visible_Color");
							if (T.GetPriority(steamPlayer.playerID.steamID.m_SteamID) == Priority.Friendly)
							{
								color = Colors.GetColor("Friendly_Chams_Occluded_Color");
								color2 = Colors.GetColor("Friendly_Chams_Visible_Color");
							}
							if (!G.BeingSpied)
							{
								ESP.ApplyChams(espobj7, color2, color);
							}
							list.Add(steamPlayer);
						}
					}
					T.ConnectedPlayers = list.ToArray();
					ESP.EObjects = list2;
				}
				yield return new WaitForSeconds(4f);
			}
			yield break;
		}
	}
}
