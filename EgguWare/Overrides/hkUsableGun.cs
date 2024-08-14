using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EgguWare.Attributes;
using EgguWare.Cheats;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Overrides
{
	[Comp]
	public class hkUsableGun : MonoBehaviour
	{
		private void Start()
		{
			hkUsableGun.BulletsField = typeof(UseableGun).GetField("bullets", BindingFlags.Instance | BindingFlags.NonPublic);
		}
		public void OV_ballistics()
		{
			Useable useable = Player.player.equipment.useable;
			ItemGunAsset itemGunAsset = (ItemGunAsset)Player.player.equipment.asset;
			PlayerLook look = Player.player.look;
			if (itemGunAsset.projectile != null)
			{
				return;
			}
			List<BulletInfo> list = (List<BulletInfo>)hkUsableGun.BulletsField.GetValue(useable);
			if (list.Count == 0)
			{
				return;
			}
			Transform transform = ((Player.player.look.perspective == EPlayerPerspective.FIRST) ? Player.player.look.aim : G.MainCamera.transform);
			RaycastInfo raycastInfo = hkDamageTool.SetupRaycast(new Ray(transform.position, transform.forward), T.GetGunDistance().Value, RayMasks.DAMAGE_CLIENT, null);
			if (Provider.modeConfigData.Gameplay.Ballistics)
			{
				for (int i = 0; i < list.Count; i++)
				{
					BulletInfo bulletInfo = list[i];
					double num = (double)Vector3.Distance(Player.player.transform.position, raycastInfo.point);
					if ((double)((float)bulletInfo.steps * itemGunAsset.ballisticTravel) >= num)
					{
						EPlayerHit eplayerHit = hkUsableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
						PlayerUI.hitmark(0, Vector3.zero, false, eplayerHit);
						Player.player.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
						Weapons.AddTracer(raycastInfo);
						Weapons.AddDamage(raycastInfo);
						bulletInfo.steps = 254;
					}
				}
				for (int j = list.Count - 1; j >= 0; j--)
				{
					BulletInfo bulletInfo2 = list[j];
					bulletInfo2.steps += 1;
					if (bulletInfo2.steps >= itemGunAsset.ballisticSteps)
					{
						list.RemoveAt(j);
					}
				}
				return;
			}
			for (int k = 0; k < list.Count; k++)
			{
				EPlayerHit eplayerHit2 = hkUsableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
				PlayerUI.hitmark(0, Vector3.zero, false, eplayerHit2);
				Player.player.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
				Weapons.AddTracer(raycastInfo);
				Weapons.AddDamage(raycastInfo);
			}
			list.Clear();
		}

		public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
		{
			EPlayerHit eplayerHit = EPlayerHit.NONE;
			if (ri == null || PAsset == null)
			{
				return eplayerHit;
			}
			if (ri.animal || ri.player || ri.zombie)
			{
				eplayerHit = EPlayerHit.ENTITIY;
				if (ri.limb == ELimb.SKULL)
				{
					eplayerHit = EPlayerHit.CRITICAL;
				}
			}
			else if (ri.transform)
			{
				if (ri.transform.CompareTag("Barricade") && PAsset.barricadeDamage > 1f)
				{
					InteractableDoorHinge component = ri.transform.GetComponent<InteractableDoorHinge>();
					if (component != null)
					{
						ri.transform = component.transform.parent.parent;
					}
					ushort num;
					if (!ushort.TryParse(ri.transform.name, out num))
					{
						return eplayerHit;
					}
					ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, num);
					if (itemBarricadeAsset == null || (!itemBarricadeAsset.isVulnerable && !PAsset.isInvulnerable))
					{
						return eplayerHit;
					}
					if (eplayerHit == EPlayerHit.NONE)
					{
						eplayerHit = EPlayerHit.BUILD;
					}
				}
				else if (ri.transform.CompareTag("Structure") && PAsset.structureDamage > 1f)
				{
					ushort num2;
					if (!ushort.TryParse(ri.transform.name, out num2))
					{
						return eplayerHit;
					}
					ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, num2);
					if (itemStructureAsset == null || (!itemStructureAsset.isVulnerable && !PAsset.isInvulnerable))
					{
						return eplayerHit;
					}
					if (eplayerHit == EPlayerHit.NONE)
					{
						eplayerHit = EPlayerHit.BUILD;
					}
				}
				else if (ri.transform.CompareTag("Resource") && PAsset.resourceDamage > 1f)
				{
					byte b;
					byte b2;
					ushort num3;
					if (!ResourceManager.tryGetRegion(ri.transform, out b, out b2, out num3))
					{
						return eplayerHit;
					}
					ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(b, b2, num3);
					if (resourceSpawnpoint == null || resourceSpawnpoint.isDead || !PAsset.bladeIDs.Contains(resourceSpawnpoint.asset.bladeID))
					{
						return eplayerHit;
					}
					if (eplayerHit == EPlayerHit.NONE)
					{
						eplayerHit = EPlayerHit.BUILD;
					}
				}
				else if (PAsset.objectDamage > 1f)
				{
					InteractableObjectRubble component2 = ri.transform.GetComponent<InteractableObjectRubble>();
					if (component2 == null)
					{
						return eplayerHit;
					}
					ri.section = component2.getSection(ri.collider.transform);
					if (component2.isSectionDead(ri.section) || (!component2.asset.rubbleIsVulnerable && !PAsset.isInvulnerable))
					{
						return eplayerHit;
					}
					if (eplayerHit == EPlayerHit.NONE)
					{
						eplayerHit = EPlayerHit.BUILD;
					}
				}
			}
			else if (ri.vehicle && !ri.vehicle.isDead && PAsset.vehicleDamage > 1f && ri.vehicle.asset != null && (ri.vehicle.asset.isVulnerable || PAsset.isInvulnerable) && eplayerHit == EPlayerHit.NONE)
			{
				eplayerHit = EPlayerHit.BUILD;
			}
			return eplayerHit;
		}

		private static FieldInfo BulletsField;
	}
}
