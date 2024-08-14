using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Classes
{

	public class ItemClumpObject
	{

		public ItemClumpObject(List<InteractableItem> items, Vector3 pos)
		{
			this.ClumpedItems = items;
			this.WorldPos = pos;
		}

		public List<InteractableItem> ClumpedItems;

		public Vector3 WorldPos;
	}
}
