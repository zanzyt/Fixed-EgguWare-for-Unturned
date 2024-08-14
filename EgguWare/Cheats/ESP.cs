using System;
using System.Collections.Generic;
using EgguWare.Attributes;
using EgguWare.Classes;
using EgguWare.Utilities;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class ESP : MonoBehaviour
	{
		[Obsolete]
		private void OnGUI()
		{
			if (!Provider.isConnected || Provider.isLoading || (PlayerCam.IsFullScreen && PlayerCam.player != null))
			{
				return;
			}
			if (G.Settings.ItemOptions.Enabled && G.Settings.GlobalOptions.ListClumpedItems && !G.BeingSpied)
			{
				for (int i = 0; i < ESP.ItemClumps.Count; i++)
				{
					ItemClumpObject itemClumpObject = ESP.ItemClumps[i];
					Vector3 vector = G.MainCamera.WorldToScreenPoint(itemClumpObject.WorldPos);
					vector.y = (float)Screen.height - vector.y;
					if (vector.z >= 0f && Vector3.Distance(Player.player.transform.position, itemClumpObject.WorldPos) <= (float)G.Settings.ItemOptions.MaxDistance)
					{
						string text = "";
						foreach (InteractableItem interactableItem in itemClumpObject.ClumpedItems)
						{
							Color rarityColorHighlight = ItemTool.getRarityColorHighlight(interactableItem.asset.rarity);
							Color32 color = new Color32((byte)(rarityColorHighlight.r * 255f), (byte)(rarityColorHighlight.g * 255f), (byte)(rarityColorHighlight.b * 255f), byte.MaxValue);
							text = string.Concat(new string[]
							{
								text,
								"<color=#",
								Colors.ColorToHex(color),
								">",
								interactableItem.asset.itemName,
								"</color>\n"
							});
						}
						Vector2 vector2 = GUIStyle.none.CalcSize(new GUIContent("<size=10>" + text + "</size>"));
						GUILayout.BeginArea(new Rect(vector.x, vector.y, vector2.x + 10f, vector2.y), "box");
						GUILayout.Label("<size=10>" + text + "</size>", Array.Empty<GUILayoutOption>());
						GUILayout.EndArea();
					}
				}
			}
			GUI.skin = null;
			for (int j = 0; j < ESP.EObjects.Count; j++)
			{
				ESPObj espobj = ESP.EObjects[j];
				if (espobj.GObject != null && (!espobj.Options.Enabled || T.GetDistance(espobj.GObject.transform.position) > (float)espobj.Options.MaxDistance || (espobj.Target == ESPObject.Item && (!T.IsItemWhitelisted((InteractableItem)espobj.Object, G.Settings.MiscOptions.ESPWhitelist) || Items.IsAlreadyClumped((InteractableItem)espobj.Object)))))
				{
					Highlighter component = espobj.GObject.GetComponent<Highlighter>();
					if (component != null)
					{
						component.ConstantOffImmediate();
					}
				}
				if (!(espobj.GObject == null) && T.InScreenView(G.MainCamera.WorldToViewportPoint(espobj.GObject.transform.position)) && espobj.Options.Enabled && T.GetDistance(espobj.GObject.transform.position) <= (float)espobj.Options.MaxDistance)
				{
					if (G.BeingSpied)
					{
						Highlighter component2 = espobj.GObject.GetComponent<Highlighter>();
						if (component2 != null)
						{
							component2.ConstantOffImmediate();
						}
						T.RemoveShaders(espobj.GObject);
					}
					else if ((espobj.Target != ESPObject.Player || !((SteamPlayer)espobj.Object).player.life.isDead) && (espobj.Target != ESPObject.Zombie || !((Zombie)espobj.Object).isDead) && (espobj.Target != ESPObject.Vehicle || !((InteractableVehicle)espobj.Object).isDead) && (espobj.Target != ESPObject.Vehicle || !G.Settings.GlobalOptions.OnlyUnlocked || !((InteractableVehicle)espobj.Object).isLocked) && (espobj.Target != ESPObject.Storage || !G.Settings.GlobalOptions.OnlyUnclaimed || !((InteractableBed)espobj.Object).isClaimed) && (espobj.Target != ESPObject.Item || T.IsItemWhitelisted((InteractableItem)espobj.Object, G.Settings.MiscOptions.ESPWhitelist)) && (espobj.Target != ESPObject.Item || !G.Settings.GlobalOptions.ListClumpedItems || !Items.IsAlreadyClumped((InteractableItem)espobj.Object)))
					{
						if (G.BeingSpied)
						{
							Highlighter component3 = espobj.GObject.GetComponent<Highlighter>();
							if (component3 != null)
							{
								component3.ConstantOffImmediate();
							}
							T.RemoveShaders(espobj.GObject);
						}
						else
						{
							string text2 = string.Format("<size={0}>", espobj.Options.FontSize);
							string text3 = string.Format("<size={0}>", espobj.Options.FontSize);
							Color32 color2 = Colors.GetColor(Enum.GetName(typeof(ESPObject), espobj.Target) + "_ESP");
							if (espobj.Options.Distance)
							{
								text2 += string.Format("<color=white>[{0}]</color> ", T.GetDistance(espobj.GObject.transform.position));
								text3 += string.Format("[{0}] ", T.GetDistance(espobj.GObject.transform.position));
							}
							switch (espobj.Target)
							{
							case ESPObject.Player:
							{
								Player player = ((SteamPlayer)espobj.Object).player;
								Priority priority = T.GetPriority(((SteamPlayer)espobj.Object).playerID.steamID.m_SteamID);
								if (priority != Priority.Friendly)
								{
									if (priority == Priority.Marked)
									{
										color2 = Colors.GetColor("Marked_Player_ESP");
									}
								}
								else
								{
									color2 = Colors.GetColor("Friendly_Player_ESP");
								}
								if (espobj.Options.Name)
								{
									text2 += ((SteamPlayer)espobj.Object).playerID.characterName;
									text3 += ((SteamPlayer)espobj.Object).playerID.characterName;
								}
								if (G.Settings.GlobalOptions.Weapon)
								{
									string text4 = ((player.equipment.asset != null) ? ((SteamPlayer)espobj.Object).player.equipment.asset.itemName : "None");
									text2 = text2 + "<color=white> - " + text4 + "</color>";
									text3 = text3 + " - " + text4;
								}
								if (G.Settings.GlobalOptions.ViewHitboxes && G.Settings.AimbotOptions.ExpandHitboxes && G.Settings.AimbotOptions.SilentAim)
								{
									Player player2 = ((SteamPlayer)espobj.Object).player;
									Vector3 vector3 = T.WorldToScreen(player.transform.position);
									if (vector3.z >= 0f)
									{
										Vector3 vector4 = T.WorldToScreen(new Vector3(player.transform.position.x, player.transform.position.y + (float)G.Settings.AimbotOptions.HitboxSize, player.transform.position.z));
										float num = Vector3.Distance(vector3, vector4);
										T.DrawCircle(Colors.GetColor("Extended_Hitbox_Circle"), new Vector2(vector3.x, vector3.y), num);
									}
								}
								break;
							}
							case ESPObject.Vehicle:
								if (espobj.Options.Name)
								{
									text2 += ((InteractableVehicle)espobj.Object).asset.vehicleName;
									text3 += ((InteractableVehicle)espobj.Object).asset.vehicleName;
								}
								if (G.Settings.GlobalOptions.VehicleLocked)
								{
									if (((InteractableVehicle)espobj.Object).isLocked)
									{
										text2 += "<color=white> - Locked</color>";
										text3 += " - Locked";
									}
									else
									{
										text2 += "<color=white> - </color><color=ff5a00>Unlocked</color>";
										text3 += " - Unlocked";
									}
								}
								break;
							case ESPObject.Item:
								if (espobj.Options.Name)
								{
									text2 += ((InteractableItem)espobj.Object).asset.itemName;
									text3 += ((InteractableItem)espobj.Object).asset.itemName;
								}
								break;
							case ESPObject.Zombie:
								goto IL_0AED;
							case ESPObject.Ladder:
							{
								BarricadeData barricadeData = null;
								if (espobj.Options.Name || G.Settings.GlobalOptions.ShowLocked)
								{
									try
									{
										byte b;
										byte b2;
										ushort num2;
										ushort num3;
										BarricadeRegion barricadeRegion;
										if (BarricadeManager.tryGetInfo(((InteractableStorage)espobj.Object).transform, out b, out b2, out num2, out num3, out barricadeRegion))
										{
											barricadeData = barricadeRegion.barricades[(int)num3];
										}
									}
									catch (Exception ex)
									{
										Debug.Log(ex);
									}
								}
								if (espobj.Options.Name)
								{
									string text5 = "Storage";
									if (barricadeData != null)
									{
										text5 = barricadeData.barricade.asset.name.Replace("_", " ");
									}
									text2 += text5;
									text3 += text5;
								}
								if (G.Settings.GlobalOptions.ShowLocked)
								{
									if (barricadeData != null)
									{
										if (barricadeData.barricade.asset.isLocked)
										{
											text2 += "<color=white> - Locked</color>";
											text3 += " - Locked";
										}
										else
										{
											text2 += "<color=white> - </color><color=ff5a00>Unlocked</color>";
											text3 += " - Unlocked";
										}
									}
									else
									{
										text2 += "<color=white> - Unknown</color>";
										text3 += " - Unknown";
									}
								}
								break;
							}
							case ESPObject.Storage:
								if (espobj.Options.Name)
								{
									text2 += Enum.GetName(typeof(ESPObject), espobj.Target);
									text3 += Enum.GetName(typeof(ESPObject), espobj.Target);
								}
								if (G.Settings.GlobalOptions.Claimed)
								{
									if (((InteractableBed)espobj.Object).isClaimed)
									{
										text2 += "<color=white> - Claimed</color>";
										text3 += " - Claimed";
									}
									else
									{
										text2 += "<color=white> - </color><color=ff5a00>Unclaimed</color>";
										text3 += " - Unclaimed";
									}
								}
								break;
							default:
								goto IL_0AED;
							}
							IL_0B43:
							text2 += "</size>";
							text3 += "</size>";
							if (espobj.Options.Tracers)
							{
								T.DrawSnapline(espobj.GObject.transform.position, color2);
							}
							if (!string.IsNullOrEmpty(text2))
							{
								T.DrawESPLabel(espobj.GObject.transform.position, color2, Color.black, text2, text3);
							}
							if (espobj.Options.Box)
							{
								if (espobj.Target == ESPObject.Player)
								{
									Vector3 position = espobj.GObject.transform.position;
									Vector3 localScale = espobj.GObject.transform.localScale;
									T.Draw3DBox(new Bounds(position + new Vector3(0f, 1.1f, 0f), localScale + new Vector3(0f, 0.95f, 0f)), color2);
								}
								else
								{
									T.Draw3DBox(espobj.GObject.GetComponent<Collider>().bounds, color2);
								}
							}
							if (espobj.Options.Glow)
							{
								Highlighter highlighter = espobj.GObject.GetComponent<Highlighter>() ?? espobj.GObject.AddComponent<Highlighter>();
								highlighter.occluder = true;
								highlighter.overlay = true;
								highlighter.ConstantOnImmediate(color2);
								goto IL_0CC4;
							}
							Highlighter component4 = espobj.GObject.GetComponent<Highlighter>();
							if (component4 != null)
							{
								component4.ConstantOffImmediate();
								goto IL_0CC4;
							}
							goto IL_0CC4;
							IL_0AED:
							if (espobj.Options.Name)
							{
								text2 += Enum.GetName(typeof(ESPObject), espobj.Target);
								text3 += Enum.GetName(typeof(ESPObject), espobj.Target);
								goto IL_0B43;
							}
							goto IL_0B43;
						}
					}
				}
				IL_0CC4:;
			}
			Player player3 = Player.player;
			Highlighter highlighter2;
			if (player3 == null)
			{
				highlighter2 = null;
			}
			else
			{
				GameObject gameObject = player3.gameObject;
				highlighter2 = ((gameObject != null) ? gameObject.GetComponent<Highlighter>() : null);
			}
			Highlighter highlighter3 = highlighter2;
			if (highlighter3 != null)
			{
				highlighter3.ConstantOffImmediate();
			}
		}

		public static void ApplyChams(ESPObj gameObject, Color vis, Color invis)
		{
			ShaderType chamType = gameObject.Options.ChamType;
			if (chamType == ShaderType.Material)
			{
				T.ApplyShader(AssetUtilities.Shaders["chamsLit"], gameObject.GObject, vis, invis);
				return;
			}
			if (chamType == ShaderType.Flat)
			{
				T.ApplyShader(AssetUtilities.Shaders["Chams"], gameObject.GObject, vis, invis);
				return;
			}
			T.RemoveShaders(gameObject.GObject);
		}

		public static List<ItemClumpObject> ItemClumps = new List<ItemClumpObject>();

		public static List<ESPObj> EObjects = new List<ESPObj>();

		private Vector2 scroll = new Vector2(0f, 0f);
	}
}
