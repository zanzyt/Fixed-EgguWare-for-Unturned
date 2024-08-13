using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using EgguWare.Classes;
using EgguWare.Menu;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{
	// Token: 0x02000013 RID: 19
	public class T
	{
		// Token: 0x0600004D RID: 77
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetCurrentHwProfile(IntPtr fProfile);

		// Token: 0x0600004E RID: 78 RVA: 0x00004078 File Offset: 0x00002278
		public static SteamPlayer GetSteamPlayer(Player player)
		{
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (steamPlayer.player == player)
				{
					return steamPlayer;
				}
			}
			return null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000040D8 File Offset: 0x000022D8
		public static bool InScreenView(Vector3 scrnpt)
		{
			return scrnpt.z > 0f && scrnpt.x > 0f && scrnpt.x < 1f && scrnpt.y > 0f && scrnpt.y < 1f;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000022AB File Offset: 0x000004AB
		public static float GetDistance(Vector3 endpos)
		{
			return (float)Math.Round((double)Vector3.Distance(Player.player.look.aim.position, endpos));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004128 File Offset: 0x00002328
		public static bool VisibleFromCamera(Vector3 pos)
		{
			Vector3 normalized = (pos - MainCamera.instance.transform.position).normalized;
			RaycastHit raycastHit;
			Physics.Raycast(MainCamera.instance.transform.position, normalized, out raycastHit, float.PositiveInfinity, RayMasks.DAMAGE_CLIENT);
			return DamageTool.getPlayer(raycastHit.transform);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004188 File Offset: 0x00002388
		public static void AimAt(Vector3 pos)
		{
			Player.player.transform.LookAt(pos);
			Player.player.transform.eulerAngles = new Vector3(0f, Player.player.transform.rotation.eulerAngles.y, 0f);
			Camera.main.transform.LookAt(pos);
			float num = Camera.main.transform.localRotation.eulerAngles.x;
			if (num <= 90f && num <= 270f)
			{
				num = Camera.main.transform.localRotation.eulerAngles.x + 90f;
			}
			else if (num >= 270f && num <= 360f)
			{
				num = Camera.main.transform.localRotation.eulerAngles.x - 270f;
			}
			Player.player.look.GetType().GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Player.player.look, num);
			Player.player.look.GetType().GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Player.player.look, Player.player.transform.rotation.eulerAngles.y);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000022CE File Offset: 0x000004CE
		public static IEnumerator Notify(string message, Color color, float displayTime)
		{
			float started = Time.realtimeSinceStartup;
			do
			{
				yield return new WaitForEndOfFrame();
				PlayerUI.hint(null, EPlayerMessage.INTERACT, message, color, Array.Empty<object>());
			}
			while (Time.realtimeSinceStartup - started <= displayTime);
			yield break;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000042F0 File Offset: 0x000024F0
		public static void DrawSnapline(Vector3 worldpos, Color color)
		{
			Vector3 vector = G.MainCamera.WorldToScreenPoint(worldpos);
			vector.y = (float)Screen.height - vector.y;
			GL.PushMatrix();
			GL.Begin(1);
			T.DrawMaterial.SetPass(0);
			GL.Color(color);
			GL.Vertex3((float)(Screen.width / 2), (float)Screen.height, 0f);
			GL.Vertex3(vector.x, vector.y, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004374 File Offset: 0x00002574
		public static void DrawESPLabel(Vector3 worldpos, Color textcolor, Color outlinecolor, string text, string outlinetext = null)
		{
			GUIContent guicontent = new GUIContent(text);
			if (outlinetext == null)
			{
				outlinetext = text;
			}
			GUIContent guicontent2 = new GUIContent(outlinetext);
			GUIStyle label = GUI.skin.label;
			label.alignment = TextAnchor.MiddleCenter;
			Vector2 vector = label.CalcSize(guicontent);
			Vector3 vector2 = G.MainCamera.WorldToScreenPoint(worldpos);
			vector2.y = (float)Screen.height - vector2.y;
			if (vector2.z >= 0f)
			{
				GUI.color = Color.black;
				GUI.Label(new Rect(vector2.x - vector.x / 2f + 1f, vector2.y + 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f - 1f, vector2.y - 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f + 1f, vector2.y - 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f - 1f, vector2.y + 1f, vector.x, vector.y), guicontent2);
				GUI.color = textcolor;
				GUI.Label(new Rect(vector2.x - vector.x / 2f, vector2.y, vector.x, vector.y), guicontent);
				GUI.color = Main.GUIColor;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000451C File Offset: 0x0000271C
		public static Vector3 WorldToScreen(Vector3 worldpos)
		{
			Vector3 vector = G.MainCamera.WorldToScreenPoint(worldpos);
			vector.y = (float)Screen.height - vector.y;
			return new Vector3(vector.x, vector.y);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000455C File Offset: 0x0000275C
		public static void DrawOutlineLabel(Vector2 rect, Color textcolor, Color outlinecolor, string text, string outlinetext = null)
		{
			GUIContent guicontent = new GUIContent(text);
			if (outlinetext == null)
			{
				outlinetext = text;
			}
			GUIContent guicontent2 = new GUIContent(outlinetext);
			Vector2 vector = GUI.skin.label.CalcSize(guicontent);
			GUI.color = Color.black;
			GUI.Label(new Rect(rect.x + 1f, rect.y + 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x - 1f, rect.y - 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x + 1f, rect.y - 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x - 1f, rect.y + 1f, vector.x, vector.y), guicontent2);
			GUI.color = textcolor;
			GUI.Label(new Rect(rect.x, rect.y, vector.x, vector.y), guicontent);
			GUI.color = Main.GUIColor;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000468C File Offset: 0x0000288C
		public static void Draw3DBox(Bounds b, Color color)
		{
			Vector3[] array = new Vector3[]
			{
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				G.MainCamera.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z))
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].y = (float)Screen.height - array[i].y;
			}
			GL.PushMatrix();
			GL.Begin(1);
			T.DrawMaterial.SetPass(0);
			GL.End();
			GL.PopMatrix();
			GL.PushMatrix();
			GL.Begin(1);
			T.DrawMaterial.SetPass(0);
			GL.Color(color);
			GL.Vertex3(array[0].x, array[0].y, 0f);
			GL.Vertex3(array[1].x, array[1].y, 0f);
			GL.Vertex3(array[1].x, array[1].y, 0f);
			GL.Vertex3(array[5].x, array[5].y, 0f);
			GL.Vertex3(array[5].x, array[5].y, 0f);
			GL.Vertex3(array[4].x, array[4].y, 0f);
			GL.Vertex3(array[4].x, array[4].y, 0f);
			GL.Vertex3(array[0].x, array[0].y, 0f);
			GL.Vertex3(array[2].x, array[2].y, 0f);
			GL.Vertex3(array[3].x, array[3].y, 0f);
			GL.Vertex3(array[3].x, array[3].y, 0f);
			GL.Vertex3(array[7].x, array[7].y, 0f);
			GL.Vertex3(array[7].x, array[7].y, 0f);
			GL.Vertex3(array[6].x, array[6].y, 0f);
			GL.Vertex3(array[6].x, array[6].y, 0f);
			GL.Vertex3(array[2].x, array[2].y, 0f);
			GL.Vertex3(array[2].x, array[2].y, 0f);
			GL.Vertex3(array[0].x, array[0].y, 0f);
			GL.Vertex3(array[3].x, array[3].y, 0f);
			GL.Vertex3(array[1].x, array[1].y, 0f);
			GL.Vertex3(array[7].x, array[7].y, 0f);
			GL.Vertex3(array[5].x, array[5].y, 0f);
			GL.Vertex3(array[6].x, array[6].y, 0f);
			GL.Vertex3(array[4].x, array[4].y, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000022EB File Offset: 0x000004EB
		public static void DrawColor(Rect position, Color color)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(position, GUIContent.none, T.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000230D File Offset: 0x0000050D
		public static void DrawColorLayout(Color color, GUILayoutOption[] options = null)
		{
			GUI.skin = AssetUtilities.Skin;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUILayout.Button(" ", T.textureStyle, options);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004D50 File Offset: 0x00002F50
		public static Priority GetPriority(ulong id)
		{
			Priority priority;
			G.Settings.Priority.TryGetValue(id, out priority);
			return priority;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004D74 File Offset: 0x00002F74
		public static void ApplyShader(Shader shader, GameObject pgo, Color32 VisibleColor, Color32 OccludedColor)
		{
			if (shader == null)
			{
				return;
			}
			Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Material[] materials = componentsInChildren[i].materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].shader = shader;
					materials[j].SetColor("_ColorVisible", VisibleColor);
					materials[j].SetColor("_ColorBehind", OccludedColor);
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public static void Log(string s)
		{
			File.AppendAllText("EgguWare.log", string.Concat(new string[]
			{
				"[",
				DateTime.Now.ToLongTimeString(),
				"] ",
				s,
				Environment.NewLine
			}));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004E38 File Offset: 0x00003038
		public static void RemoveShaders(GameObject pgo)
		{
			if (Shader.Find("Standard") == null)
			{
				return;
			}
			Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.shader != Shader.Find("Standard"))
				{
					Material[] materials = componentsInChildren[i].materials;
					for (int j = 0; j < materials.Length; j++)
					{
						materials[j].shader = Shader.Find("Standard");
					}
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004EB4 File Offset: 0x000030B4
		public static Vector3 GetLimbPosition(Transform target, string objName)
		{
			Transform[] componentsInChildren = target.transform.GetComponentsInChildren<Transform>();
			Vector3 vector = Vector3.zero;
			if (componentsInChildren == null)
			{
				return vector;
			}
			foreach (Transform transform in componentsInChildren)
			{
				if (!(transform.name.Trim() != objName))
				{
					vector = transform.position + new Vector3(0f, 0.4f, 0f);
					break;
				}
			}
			return vector;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004F28 File Offset: 0x00003128
		public static void OverrideMethod(Type defaultClass, Type overrideClass, string method, BindingFlags bindingflag, BindingFlags overrideflag)
		{
			string text = "OV_" + method;
			MemberInfo[] member = defaultClass.GetMember(method, MemberTypes.Method, bindingflag);
			if (member == null || member.Length == 0)
			{
				T.Log("Original method not found: " + defaultClass.AssemblyQualifiedName + "." + method);
				return;
			}
			OverrideHelper.RedirectCalls((MethodInfo)member[0], overrideClass.GetMethod(text, overrideflag));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004F84 File Offset: 0x00003184
		public static void OverrideMethod(Type defaultClass, Type overrideClass, string method, BindingFlags bindingflag, BindingFlags overrideflag, Type[] args)
		{
			string text = "OV_" + method;
			MethodInfo method2 = defaultClass.GetMethod(method, bindingflag, null, args, null);
			if (method2 == null)
			{
				T.Log("Original method not found: " + defaultClass.AssemblyQualifiedName + "." + method);
				return;
			}
			OverrideHelper.RedirectCalls(method2, overrideClass.GetMethod(text, overrideflag));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004FE0 File Offset: 0x000031E0
		public static float? GetGunDistance()
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
			ItemGunAsset itemGunAsset = obj as ItemGunAsset;
			return new float?((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005038 File Offset: 0x00003238
		public static float GetDamage(Player player, ELimb limb)
		{
			ItemGunAsset itemGunAsset = Player.player.equipment.asset as ItemGunAsset;
			return itemGunAsset.objectDamage * itemGunAsset.playerDamageMultiplier.damage * DamageTool.getPlayerArmor(limb, player);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005074 File Offset: 0x00003274
		public static bool IsItemWhitelisted(InteractableItem item, ItemWhitelistObject itemWhitelistObject)
		{
			return !itemWhitelistObject.filterItems || (itemWhitelistObject.allowGun && item.asset is ItemGunAsset) || (itemWhitelistObject.allowBackpack && item.asset is ItemBackpackAsset) || (itemWhitelistObject.allowAmmo && (item.asset is ItemMagazineAsset || item.asset is ItemCaliberAsset)) || (itemWhitelistObject.allowAttachments && (item.asset is ItemBarrelAsset || item.asset is ItemOpticAsset)) || (itemWhitelistObject.allowClothing && item.asset is ItemClothingAsset) || (itemWhitelistObject.allowFuel && item.asset is ItemFuelAsset) || (itemWhitelistObject.allowMedical && item.asset is ItemMedicalAsset) || (itemWhitelistObject.allowMelee && item.asset is ItemMeleeAsset) || (itemWhitelistObject.allowThrowable && item.asset is ItemThrowableAsset) || (itemWhitelistObject.allowFoodWater && (item.asset is ItemFoodAsset || item.asset is ItemWaterAsset));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000051A0 File Offset: 0x000033A0
		public static ELimb GetLimb(TargetLimb limb)
		{
			if (limb == TargetLimb.GLOBAL)
			{
				return ELimb.SKULL;
			}
			if (limb == TargetLimb.RANDOM)
			{
				ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
				return array[T.Random.Next(0, array.Length)];
			}
			return (ELimb)Enum.Parse(typeof(TargetLimb), Enum.GetName(typeof(TargetLimb), limb));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000520C File Offset: 0x0000340C
		public static Player GetNearestPlayer(int? pixelfov = null, int? distance = null)
		{
			Player player = null;
			for (int i = 0; i < T.ConnectedPlayers.Length; i++)
			{
				SteamPlayer steamPlayer = T.ConnectedPlayers[i];
				if (steamPlayer != null && !(steamPlayer.playerID.steamID == Provider.client) && !steamPlayer.player.life.isDead)
				{
					if (distance != null)
					{
						int num = (int)Vector3.Distance(Player.player.look.aim.position, steamPlayer.player.transform.position);
						int? num2 = distance;
						if ((num > num2.GetValueOrDefault()) & (num2 != null))
						{
							goto IL_01C0;
						}
					}
					if (T.GetPriority(steamPlayer.playerID.steamID.m_SteamID) != Priority.Friendly)
					{
						Vector3 vector = G.MainCamera.WorldToScreenPoint(T.GetLimbPosition(steamPlayer.player.transform, "Skull"));
						if (vector.z > 0f)
						{
							int num3 = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector.x, vector.y));
							if (pixelfov != null)
							{
								int num4 = num3;
								int? num5 = pixelfov;
								if ((num4 > num5.GetValueOrDefault()) & (num5 != null))
								{
									goto IL_01C0;
								}
							}
							if (player == null)
							{
								player = steamPlayer.player;
							}
							else
							{
								Vector3 vector2 = G.MainCamera.WorldToScreenPoint(T.GetLimbPosition(player.transform, "Skull"));
								int num6 = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector2.x, vector2.y));
								if (pixelfov != null)
								{
									int num7 = num6;
									int? num8 = pixelfov;
									if ((num7 > num8.GetValueOrDefault()) & (num8 != null))
									{
										player = null;
									}
								}
								if (num3 < num6)
								{
									player = steamPlayer.player;
								}
							}
						}
					}
				}
				IL_01C0:;
			}
			return player;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000053EC File Offset: 0x000035EC
		public static InteractableItem GetNearestItem(int? pixelfov = null)
		{
			InteractableItem interactableItem = null;
			foreach (Collider collider in Physics.OverlapSphere(Player.player.transform.position, 19f, 8192))
			{
				if (!(collider == null) && !(collider.GetComponent<InteractableItem>() == null) && collider.GetComponent<InteractableItem>().asset != null)
				{
					InteractableItem component = collider.GetComponent<InteractableItem>();
					Vector3 vector = G.MainCamera.WorldToScreenPoint(component.transform.position);
					if (vector.z > 0f)
					{
						int num = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector.x, vector.y));
						if (pixelfov != null)
						{
							int num2 = num;
							int? num3 = pixelfov;
							if ((num2 > num3.GetValueOrDefault()) & (num3 != null))
							{
								goto IL_015F;
							}
						}
						if (interactableItem == null)
						{
							interactableItem = component;
						}
						else
						{
							Vector3 vector2 = G.MainCamera.WorldToScreenPoint(interactableItem.transform.position);
							int num4 = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector2.x, vector2.y));
							if (pixelfov != null)
							{
								int num5 = num4;
								int? num6 = pixelfov;
								if ((num5 > num6.GetValueOrDefault()) & (num6 != null))
								{
									interactableItem = null;
								}
							}
							if (num < num4)
							{
								interactableItem = component;
							}
						}
					}
				}
				IL_015F:;
			}
			return interactableItem;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005568 File Offset: 0x00003768
		public static void DrawCircle(Color Col, Vector2 Center, float Radius)
		{
			GL.PushMatrix();
			T.DrawMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(Col);
			for (float num = 0f; num < 6.2831855f; num += 0.05f)
			{
				GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
				GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000233A File Offset: 0x0000053A
		public static IEnumerator CheckVerification(Vector3 LastPos)
		{
			if (Time.realtimeSinceStartup - T.LastMovementCheck < 0.8f)
			{
				yield break;
			}
			T.LastMovementCheck = Time.realtimeSinceStartup;
			Player.player.transform.position = new Vector3(0f, -1337f, 0f);
			yield return new WaitForSeconds(3f);
			if (Vector3.Distance(Player.player.transform.position, LastPos) < 10f)
			{
				G.UnrestrictedMovement = false;
			}
			else
			{
				G.UnrestrictedMovement = true;
				Player.player.transform.position = LastPos + new Vector3(0f, 5f, 0f);
			}
			yield break;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005610 File Offset: 0x00003810
		public static string GetGunName()
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
			ItemGunAsset itemGunAsset = obj as ItemGunAsset;
			if (itemGunAsset == null)
			{
				return "Hands";
			}
			return itemGunAsset.name;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005660 File Offset: 0x00003860
		public static float? GetGunAmmo()
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
			ItemGunAsset itemGunAsset = obj as ItemGunAsset;
			return new float?((itemGunAsset != null) ? ((float)itemGunAsset.ammoMin) : 0f);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000056B8 File Offset: 0x000038B8
		public static float? GetGunAmmo2()
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
			ItemGunAsset itemGunAsset = obj as ItemGunAsset;
			return new float?((itemGunAsset != null) ? ((float)itemGunAsset.ammoMax) : 0f);
		}

		// Token: 0x04000025 RID: 37
		public static Vector2 DropdownCursorPos;

		// Token: 0x04000026 RID: 38
		public static SteamPlayer[] ConnectedPlayers;

		// Token: 0x04000027 RID: 39
		public static float LastMovementCheck;

		// Token: 0x04000028 RID: 40
		public static Material DrawMaterial;

		// Token: 0x04000029 RID: 41
		private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;

		// Token: 0x0400002A RID: 42
		private static readonly GUIStyle textureStyle = new GUIStyle
		{
			normal = new GUIStyleState
			{
				background = T.backgroundTexture
			}
		};

		// Token: 0x0400002B RID: 43
		public static global::System.Random Random = new global::System.Random();

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential)]
		private class HWProfile
		{
			// Token: 0x0400002C RID: 44
			public int dwDockInfo;

			// Token: 0x0400002D RID: 45
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
			public string szHwProfileGuid;

			// Token: 0x0400002E RID: 46
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szHwProfileName;
		}
	}
}
