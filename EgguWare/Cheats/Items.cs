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
	[Comp]
	public class Items : MonoBehaviour
	{

		private void Start()
		{
			base.StartCoroutine(this.RefreshClumpedItems());
		}

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


		private void OnGUI()
		{
			if (!G.BeingSpied && Provider.isConnected && G.Settings.MiscOptions.GrabItemThroughWalls && G.Settings.MiscOptions.LimitFOV && G.Settings.MiscOptions.DrawFOVCircle)
			{
				T.DrawCircle(Colors.GetColor("Item_FOV_Circle"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)G.Settings.MiscOptions.ItemGrabFOV);
			}
		}

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

		public static bool editingaip;
	}
}
