using System;
using EgguWare.Classes;
using UnityEngine;

namespace EgguWare.Utilities
{
	public class Colors
	{
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
		public static Color32 GetColor(string identifier)
		{
			Color32 color;
			if (G.Settings.GlobalOptions.GlobalColors.TryGetValue(identifier, out color))
			{
				return color;
			}
			return Color.magenta;
		}

		public static void AddColor(string id, Color32 c)
		{
			if (!G.Settings.GlobalOptions.GlobalColors.ContainsKey(id))
			{
				G.Settings.GlobalOptions.GlobalColors.Add(id, c);
			}
		}

		public static void SetColor(string id, Color32 c)
		{
			G.Settings.GlobalOptions.GlobalColors[id] = c;
		}

		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}
	}
}
