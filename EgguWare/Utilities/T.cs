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
	public class T
	{
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetCurrentHwProfile(IntPtr fProfile);

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

		public static bool InScreenView(Vector3 scrnpt)
		{
			return scrnpt.z > 0f && scrnpt.x > 0f && scrnpt.x < 1f && scrnpt.y > 0f && scrnpt.y < 1f;
		}

		public static float GetDistance(Vector3 endpos)
		{
			return (float)Math.Round((double)Vector3.Distance(Player.player.look.aim.position, endpos));
		}

		public static bool VisibleFromCamera(Vector3 pos)
		{
			Vector3 normalized = (pos - MainCamera.instance.transform.position).normalized;
			RaycastHit raycastHit;
			Physics.Raycast(MainCamera.instance.transform.position, normalized, out raycastHit, float.PositiveInfinity, RayMasks.DAMAGE_CLIENT);
			return DamageTool.getPlayer(raycastHit.transform);
		}

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

		public static Vector3 WorldToScreen(Vector3 worldpos)
		{
			Vector3 vector = G.MainCamera.WorldToScreenPoint(worldpos);
			vector.y = (float)Screen.height - vector.y;
			return new Vector3(vector.x, vector.y);
		}

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

		public static void DrawColor(Rect position, Color color)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(position, GUIContent.none, T.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		public static void DrawColorLayout(Color color, GUILayoutOption[] options = null)
		{
			GUI.skin = AssetUtilities.Skin;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUILayout.Button(" ", T.textureStyle, options);
			GUI.backgroundColor = backgroundColor;
		}

		public static Priority GetPriority(ulong id)
		{
			Priority priority;
			G.Settings.Priority.TryGetValue(id, out priority);
			return priority;
		}

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

		public static float GetDamage(Player player, ELimb limb)
		{
			ItemGunAsset itemGunAsset = Player.player.equipment.asset as ItemGunAsset;
			return itemGunAsset.objectDamage * itemGunAsset.playerDamageMultiplier.damage * DamageTool.getPlayerArmor(limb, player);
		}

		public static bool IsItemWhitelisted(InteractableItem item, ItemWhitelistObject itemWhitelistObject)
		{
			return !itemWhitelistObject.filterItems || (itemWhitelistObject.allowGun && item.asset is ItemGunAsset) || (itemWhitelistObject.allowBackpack && item.asset is ItemBackpackAsset) || (itemWhitelistObject.allowAmmo && (item.asset is ItemMagazineAsset || item.asset is ItemCaliberAsset)) || (itemWhitelistObject.allowAttachments && (item.asset is ItemBarrelAsset || item.asset is ItemOpticAsset)) || (itemWhitelistObject.allowClothing && item.asset is ItemClothingAsset) || (itemWhitelistObject.allowFuel && item.asset is ItemFuelAsset) || (itemWhitelistObject.allowMedical && item.asset is ItemMedicalAsset) || (itemWhitelistObject.allowMelee && item.asset is ItemMeleeAsset) || (itemWhitelistObject.allowThrowable && item.asset is ItemThrowableAsset) || (itemWhitelistObject.allowFoodWater && (item.asset is ItemFoodAsset || item.asset is ItemWaterAsset));
		}

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

		public static Vector2 DropdownCursorPos;

		public static SteamPlayer[] ConnectedPlayers;

		public static float LastMovementCheck;

		public static Material DrawMaterial;

		private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;

		private static readonly GUIStyle textureStyle = new GUIStyle
		{
			normal = new GUIStyleState
			{
				background = T.backgroundTexture
			}
		};

		public static global::System.Random Random = new global::System.Random();

		[StructLayout(LayoutKind.Sequential)]
		private class HWProfile
		{
			public int dwDockInfo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
			public string szHwProfileGuid;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szHwProfileName;
		}
	}
}
