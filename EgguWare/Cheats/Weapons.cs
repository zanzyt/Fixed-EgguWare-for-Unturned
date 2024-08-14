using System;
using System.Collections.Generic;
using System.Reflection;
using EgguWare.Attributes;
using EgguWare.Classes;
using EgguWare.Menu;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class Weapons : MonoBehaviour
	{
		public void Update()
		{
			if (Provider.isConnected && !Provider.isLoading)
			{
				Player player = Player.player;
				object obj;
				if (player == null)
				{
					obj = null;
				}
				else
				{
					PlayerEquipment equipment = player.equipment;
					obj = ((equipment != null) ? equipment.asset : null);
				}
				if (obj is ItemGunAsset)
				{
					Player player2 = Player.player;
					object obj2;
					if (player2 == null)
					{
						obj2 = null;
					}
					else
					{
						PlayerEquipment equipment2 = player2.equipment;
						obj2 = ((equipment2 != null) ? equipment2.asset : null);
					}
					ItemGunAsset itemGunAsset = (ItemGunAsset)obj2;
					if (!Weapons.SpreadBackup.ContainsKey(itemGunAsset.id))
					{
						Weapons.SpreadBackup.Add(itemGunAsset.id, itemGunAsset.spreadAim);
					}
					if (G.Settings.WeaponOptions.RemoveBurstDelay || G.Settings.WeaponOptions.RemoveHammerDelay || G.Settings.WeaponOptions.InstantReload)
					{
						Player.player.equipment.isBusy = false;
					}
					if (G.Settings.WeaponOptions.RemoveHammerDelay)
					{
						Player.player.equipment.useable.GetType().GetField("isHammering", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Player.player.equipment.useable, false);
					}
					if (G.Settings.WeaponOptions.RemoveHammerDelay)
					{
						Player.player.equipment.useable.GetType().GetField("needsRechamber", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Player.player.equipment.useable, false);
					}
					if (G.Settings.WeaponOptions.InstantReload)
					{
						Player.player.equipment.useable.GetType().GetField("reloadTime", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Player.player.equipment.useable, 0f);
					}
					if (G.Settings.WeaponOptions.FastGun)
					{
						itemGunAsset.firerate = 2;
					}
					if (G.Settings.WeaponOptions.NoSpread)
					{
						itemGunAsset.spreadAim = 0f;
						itemGunAsset.spreadCrouch = 0f;
						itemGunAsset.spreadHip = 0f;
						itemGunAsset.spreadProne = 0f;
						itemGunAsset.spreadSprint = 0f;
					}
					if (G.Settings.WeaponOptions.NoSlow)
					{
						itemGunAsset.ballisticForce = 10f;
						itemGunAsset.ballisticTravel = 10f;
						itemGunAsset.alertRadius = 0f;
						itemGunAsset.countMin = itemGunAsset.countMax;
						itemGunAsset.durability = 99f;
						itemGunAsset.aimingMovementSpeedMultiplier = 1f;
						itemGunAsset.equipableMovementSpeedMultiplier = 1f;
						itemGunAsset.damageFalloffMultiplier = 0f;
					}
					if (G.BeingSpied || !G.Settings.WeaponOptions.NoSpread)
					{
						float num;
						Weapons.SpreadBackup.TryGetValue(itemGunAsset.id, out num);
						itemGunAsset.spreadAim = num;
					}
					if (G.Settings.WeaponOptions.NoRecoil)
					{
						itemGunAsset.aimingRecoilMultiplier = 0f;
						itemGunAsset.recoilMax_x = 0f;
						itemGunAsset.recoilMax_y = 0f;
						itemGunAsset.recoilMin_x = 0f;
						itemGunAsset.recoilMin_y = 0f;
					}
					if (G.Settings.WeaponOptions.NoSway)
					{
						Player.player.animator.scopeSway = Vector3.zero;
					}
				}
			}
		}

		private void OnGUI()
		{
			if (Provider.isConnected && !Provider.isLoading)
			{
				GUI.skin = AssetUtilities.Skin;
				if (!G.BeingSpied)
				{
					if (G.Settings.WeaponOptions.WeaponInfo)
					{
						this.GunInfoWin = GUILayout.Window(6, this.GunInfoWin, new GUI.WindowFunction(this.GunInfoWindow), "\tWeapon Info", Array.Empty<GUILayoutOption>());
					}
					if (G.Settings.WeaponOptions.TracerLines && Weapons.TracerLines.Count > 0)
					{
						T.DrawMaterial.SetPass(0);
						GL.PushMatrix();
						GL.LoadProjectionMatrix(G.MainCamera.projectionMatrix);
						GL.modelview = G.MainCamera.worldToCameraMatrix;
						GL.Begin(1);
						for (int i = Weapons.TracerLines.Count - 1; i > -1; i--)
						{
							TracerObject tracerObject = Weapons.TracerLines[i];
							if (DateTime.Now - tracerObject.ShotTime > TimeSpan.FromSeconds((double)G.Settings.WeaponOptions.TracerTime))
							{
								Weapons.TracerLines.Remove(tracerObject);
							}
							else
							{
								GL.Color(Colors.GetColor("Bullet_Tracer_Color"));
								GL.Vertex(tracerObject.PlayerPos);
								GL.Vertex(tracerObject.HitPos);
							}
						}
						GL.End();
						GL.PopMatrix();
					}
					if (G.Settings.WeaponOptions.DamageIndicators && Weapons.DamageIndicators.Count > 0)
					{
						T.DrawMaterial.SetPass(0);
						for (int j = Weapons.DamageIndicators.Count - 1; j > -1; j--)
						{
							IndicatorObject indicatorObject = Weapons.DamageIndicators[j];
							if (DateTime.Now - indicatorObject.ShotTime > TimeSpan.FromSeconds(3.0))
							{
								Weapons.DamageIndicators.Remove(indicatorObject);
							}
							else
							{
								GUI.color = Color.red;
								Vector3 vector = G.MainCamera.WorldToScreenPoint(indicatorObject.HitPos + new Vector3(0f, 1f, 0f));
								vector.y = (float)Screen.height - vector.y;
								if (vector.z >= 0f)
								{
									GUIStyle label = GUI.skin.label;
									label.alignment = TextAnchor.MiddleCenter;
									Vector2 vector2 = label.CalcSize(new GUIContent(string.Format("<b>{0}</b>", indicatorObject.Damage)));
									T.DrawOutlineLabel(new Vector2(vector.x - vector2.x / 2f, vector.y - (float)(DateTime.Now - indicatorObject.ShotTime).TotalSeconds * 10f), Color.red, Color.black, string.Format("<b>{0}</b>", indicatorObject.Damage), null);
									label.alignment = TextAnchor.MiddleLeft;
								}
								GUI.color = Main.GUIColor;
							}
						}
					}
				}
			}
		}

		private void GunInfoWindow(int winid)
		{
			float valueOrDefault = T.GetGunAmmo().GetValueOrDefault(0f);
			float valueOrDefault2 = T.GetGunAmmo2().GetValueOrDefault(0f);
			GUILayout.Label("\t" + T.GetGunName(), Array.Empty<GUILayoutOption>());
			GUILayout.Space(1f);
			GUILayout.Label("\trange: " + T.GetGunDistance().ToString(), Array.Empty<GUILayoutOption>());
			GUILayout.Space(3f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(string.Format("{0}/{1}", valueOrDefault, valueOrDefault2), new GUILayoutOption[] { GUILayout.Width(100f) });
			GUILayout.EndHorizontal();
			GUILayout.HorizontalSlider(valueOrDefault2 / 2f, valueOrDefault, valueOrDefault2, new GUILayoutOption[] { GUILayout.Width(200f) });
			GUI.DragWindow();
		}

		public static void AddTracer(RaycastInfo ri)
		{
			if (G.Settings.WeaponOptions.TracerLines && ri.point != new Vector3(0f, 0f, 0f))
			{
				TracerObject tracerObject = new TracerObject
				{
					HitPos = ri.point,
					PlayerPos = Player.player.look.aim.transform.position,
					ShotTime = DateTime.Now
				};
				Weapons.TracerLines.Add(tracerObject);
			}
		}

		public static void AddDamage(RaycastInfo ri)
		{
			if (G.Settings.WeaponOptions.DamageIndicators && ri.point != new Vector3(0f, 0f, 0f))
			{
				ItemGunAsset itemGunAsset = Player.player.equipment.asset as ItemGunAsset;
				if (itemGunAsset != null && ri.player != null)
				{
					IndicatorObject indicatorObject = new IndicatorObject
					{
						HitPos = ri.point,
						Damage = Mathf.FloorToInt(DamageTool.getPlayerArmor(ri.limb, ri.player) * itemGunAsset.playerDamageMultiplier.multiply(ri.limb)),
						ShotTime = DateTime.Now
					};
					Weapons.DamageIndicators.Add(indicatorObject);
				}
			}
		}

		public Rect GunInfoWin = new Rect((float)(Screen.width - 240), 50f, 200f, 75f);

		public static List<IndicatorObject> DamageIndicators = new List<IndicatorObject>();

		public static List<TracerObject> TracerLines = new List<TracerObject>();

		public static Dictionary<ushort, float> SpreadBackup = new Dictionary<ushort, float>();
	}
}
