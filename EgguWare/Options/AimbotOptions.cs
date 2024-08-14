using System;
using System.Collections.Generic;
using EgguWare.Classes;
using UnityEngine;

namespace EgguWare.Options
{
	public class AimbotOptions
	{
		public bool SilentAim;

		public bool SilentAimInfo = true;

		public int AimpointMultiplier = 1;

		public int HitboxSize = 15;

		public TargetLimb1 TargetL = TargetLimb1.SKULL;

		public KeyCode AimlockKey = KeyCode.F;

		public List<ESPObject> SilentAimObjects = new List<ESPObject>();

		public bool OnlyVisible;

		public bool Aimlock;

		public bool Mouse1Aimbot;

		public bool AimlockLimitFOV;

		public int AimlockFOV = 200;

		public bool AimlockDrawFOV = true;

		public bool SilentAimLimitFOV;

		public int SilentAimFOV = 200;

		public bool SilentAimDrawFOV = true;

		public bool ExpandHitboxes = true;

		public int HitChance = 100;

		public bool rocketRape;

		public bool Aimlockinfo = true;
	}
}
