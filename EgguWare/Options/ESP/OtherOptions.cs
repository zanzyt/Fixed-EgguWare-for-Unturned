using System;
using System.Collections.Generic;
using UnityEngine;

namespace EgguWare.Options.ESP
{
	public class OtherOptions
	{
		public bool Claimed = true;

		public bool OnlyUnclaimed;

		public bool ListClumpedItems;

		public float DistanceThreshold = 3f;

		public int CountThreshold = 5;

		public bool Weapon = true;

		public bool ViewHitboxes = true;

		public bool VehicleLocked = true;

		public bool OnlyUnlocked;

		public bool ShowLocked = true;

		public Dictionary<string, Color32> GlobalColors = new Dictionary<string, Color32>();
	}
}
