using System;
using System.Collections;
using System.Collections.Generic;
using EgguWare.Attributes;
using EgguWare.Classes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	// Token: 0x02000044 RID: 68
	[Comp]
	public class Items : MonoBehaviour
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000025F8 File Offset: 0x000007F8
		private void Start()
		{
			base.StartCoroutine(this.RefreshClumpedItems());
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009F9C File Offset: 0x0000819C
		private void Update()
		{
			if (G.Settings.MiscOptions.AutoItemPickup)
			{
				foreach (Collider collider in Physics.OverlapSphere(Player.player.transform.position, 19f, 8192))
				{
					if (!(collider == null) && !(collider.GetComponent<InteractableItem>() == null) && collider.GetComponent<InteractableItem>().asset != null)
					{
						InteractableItem component = collider.GetComponent<InteractableItem>();
						if (T.IsItemWhitelisted(component, G.Settings.MiscOptions.AIPWhitelist))
						{
							component.use();
						}
					}
				}
			}
			if (G.Settings.MiscOptions.GrabItemThroughWalls && Input.GetKeyDown(KeyCode.F))
			{
				int? num = null;
				if (G.Settings.MiscOptions.LimitFOV)
				{
					num = new int?(G.Settings.MiscOptions.ItemGrabFOV);
				}
				InteractableItem nearestItem = T.GetNearestItem(num);
				if (nearestItem != null)
				{
					nearestItem.use();
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A098 File Offset: 0x00008298
		private void OnGUI()
		{
			if (!G.BeingSpied && Provider.isConnected && G.Settings.MiscOptions.GrabItemThroughWalls && G.Settings.MiscOptions.LimitFOV && G.Settings.MiscOptions.DrawFOVCircle)
			{
				T.DrawCircle(Colors.GetColor("Item_FOV_Circle"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)G.Settings.MiscOptions.ItemGrabFOV);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002607 File Offset: 0x00000807
		private IEnumerator RefreshClumpedItems()
		{
			for (;;)
			{
				if (G.Settings.ItemOptions.Enabled && G.Settings.GlobalOptions.ListClumpedItems)
				{
					ESP.ItemClumps.Clear();
					foreach (InteractableItem interactableItem in global::UnityEngine.Object.FindObjectsOfType<InteractableItem>())
					{
						if (T.IsItemWhitelisted(interactableItem, G.Settings.MiscOptions.ESPWhitelist) && !Items.IsAlreadyClumped(interactableItem))
						{
							Collider[] array2 = Physics.OverlapSphere(interactableItem.transform.position, G.Settings.GlobalOptions.DistanceThreshold, 8192);
							List<InteractableItem> list = new List<InteractableItem>();
							foreach (Collider collider in array2)
							{
								if (!(collider == null) && !(collider.GetComponent<InteractableItem>() == null) && collider.GetComponent<InteractableItem>().asset != null)
								{
									InteractableItem component = collider.GetComponent<InteractableItem>();
									if (T.IsItemWhitelisted(component, G.Settings.MiscOptions.ESPWhitelist) && !Items.IsAlreadyClumped(interactableItem))
									{
										list.Add(component);
									}
								}
							}
							if (list.Count >= G.Settings.GlobalOptions.CountThreshold)
							{
								ESP.ItemClumps.Add(new ItemClumpObject(list, interactableItem.transform.position));
							}
						}
					}
				}
				yield return new WaitForSeconds(4f);
			}
			yield break;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A120 File Offset: 0x00008320
		public static bool IsAlreadyClumped(InteractableItem item)
		{
			using (List<ItemClumpObject>.Enumerator enumerator = ESP.ItemClumps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ClumpedItems.Contains(item))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000123 RID: 291
		public static bool editingaip;
	}
}
