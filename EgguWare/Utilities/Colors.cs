using System;
using EgguWare.Classes;
using UnityEngine;

namespace EgguWare.Utilities
{
	// Token: 0x02000006 RID: 6
	public class Colors
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002A14 File Offset: 0x00000C14
		public static void AddColors()
		{
			foreach (object obj in Enum.GetValues(typeof(ESPObject)))
			{
				ESPObject espobject = (ESPObject)obj;
				string name = Enum.GetName(typeof(ESPObject), espobject);
				Colors.AddColor(name + "_ESP", Color.red);
				Colors.AddColor(name + "_Chams_Visible_Color", Color.yellow);
				Colors.AddColor(name + "_Chams_Occluded_Color", Color.red);
			}
			Colors.AddColor("Friendly_Player_ESP", Color.cyan);
			Colors.AddColor("Marked_Player_ESP", new Color32(byte.MaxValue, 128, 0, byte.MaxValue));
			Colors.AddColor("Chams_Visible_Color", Color.yellow);
			Colors.AddColor("Chams_Occluded_Color", Color.red);
			Colors.AddColor("Friendly_Chams_Visible_Color", Color.cyan);
			Colors.AddColor("Friendly_Chams_Occluded_Color", new Color32(0, 128, byte.MaxValue, byte.MaxValue));
			Colors.AddColor("Bullet_Tracer_Color", Color.blue);
			Colors.AddColor("Silent_Aim_FOV_Circle", Color.white);
			Colors.AddColor("Aimlock_FOV_Circle", Color.white);
			Colors.AddColor("Extended_Hitbox_Circle", Color.red);
			Colors.AddColor("Item_FOV_Circle", Color.green);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static Color32 GetColor(string identifier)
		{
			Color32 color;
			if (G.Settings.GlobalOptions.GlobalColors.TryGetValue(identifier, out color))
			{
				return color;
			}
			return Color.magenta;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020AF File Offset: 0x000002AF
		public static void AddColor(string id, Color32 c)
		{
			if (!G.Settings.GlobalOptions.GlobalColors.ContainsKey(id))
			{
				G.Settings.GlobalOptions.GlobalColors.Add(id, c);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020DE File Offset: 0x000002DE
		public static void SetColor(string id, Color32 c)
		{
			G.Settings.GlobalOptions.GlobalColors[id] = c;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020F6 File Offset: 0x000002F6
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}
	}
}
