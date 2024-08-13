using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Classes
{
	// Token: 0x0200003F RID: 63
	public class ItemClumpObject
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000025AF File Offset: 0x000007AF
		public ItemClumpObject(List<InteractableItem> items, Vector3 pos)
		{
			this.ClumpedItems = items;
			this.WorldPos = pos;
		}

		// Token: 0x0400010F RID: 271
		public List<InteractableItem> ClumpedItems;

		// Token: 0x04000110 RID: 272
		public Vector3 WorldPos;
	}
}
