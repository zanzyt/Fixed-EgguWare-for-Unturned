using System;
using EgguWare.Classes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Overrides
{
	public static class hkDamageTool
	{
		[Obsolete]
		public static RaycastInfo OV_raycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			return hkDamageTool.SetupRaycast(ray, range, mask, ignorePlayer);
		}

		[Obsolete]
		public static RaycastInfo SetupRaycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			RaycastInfo raycastInfo;
			RaycastInfo raycastInfo2;
			if (G.Settings.AimbotOptions.SilentAim && hkDamageTool.SilAimRaycast(out raycastInfo))
			{
				raycastInfo2 = raycastInfo;
			}
			else
			{
				raycastInfo2 = hkDamageTool.OriginalRaycast(ray, range, mask, ignorePlayer);
			}
			return raycastInfo2;
		}

		[Obsolete]
		public static RaycastInfo OriginalRaycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			RaycastHit raycastHit;
			Physics.Raycast(ray, out raycastHit, range, mask, QueryTriggerInteraction.UseGlobal);
			RaycastInfo raycastInfo = new RaycastInfo(raycastHit);
			raycastInfo.direction = ray.direction;
			raycastInfo.limb = ELimb.SPINE;
			if (raycastInfo.transform != null)
			{
				if (raycastInfo.transform.CompareTag("Barricade"))
				{
					raycastInfo.transform = DamageTool.getBarricadeRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Structure"))
				{
					raycastInfo.transform = DamageTool.getStructureRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Resource"))
				{
					raycastInfo.transform = DamageTool.getResourceRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Enemy"))
				{
					raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
					if (raycastInfo.player == ignorePlayer)
					{
						raycastInfo.player = null;
					}
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Zombie"))
				{
					raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Animal"))
				{
					raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Vehicle"))
				{
					raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
				}
				if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
				{
					raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
				}
				else
				{
					raycastInfo.material = DamageTool.getMaterial(raycastHit.point, raycastInfo.transform, raycastInfo.collider);
				}
			}
			return raycastInfo;
		}

		[Obsolete]
		public static bool SilAimRaycast(out RaycastInfo info)
		{
			ItemGunAsset itemGunAsset = Player.player.equipment.asset as ItemGunAsset;
			float num = ((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
			Transform transform = ((Player.player.look.perspective == EPlayerPerspective.FIRST) ? Player.player.look.aim : G.MainCamera.transform);
			info = hkDamageTool.OriginalRaycast(new Ray(transform.position, transform.forward), num, RayMasks.DAMAGE_CLIENT, null);
			int? num2 = null;
			if (G.Settings.AimbotOptions.SilentAimLimitFOV)
			{
				num2 = new int?(G.Settings.AimbotOptions.SilentAimFOV);
			}
			if (!T.GetNearestPlayer(num2, new int?((int)T.GetGunDistance().Value)))
			{
				return false;
			}
			Player nearestPlayer = T.GetNearestPlayer(null, null);
			if (G.Settings.AimbotOptions.HitChance != 100 && T.Random.Next(0, 100) >= G.Settings.AimbotOptions.HitChance)
			{
				return false;
			}
			SphereComponent component = nearestPlayer.gameObject.GetComponent<SphereComponent>();
			if (!component)
			{
				nearestPlayer.gameObject.AddComponent<SphereComponent>();
			}
			component.LastHit = Time.realtimeSinceStartup;
			Vector3 vector;
			if (T.VisibleFromCamera(T.GetLimbPosition(nearestPlayer.gameObject.transform, "Skull")))
			{
				vector = T.GetLimbPosition(nearestPlayer.gameObject.transform, "Skull");
			}
			else if (T.VisibleFromCamera(T.GetLimbPosition(nearestPlayer.gameObject.transform, "Spine")))
			{
				vector = T.GetLimbPosition(nearestPlayer.gameObject.transform, "Spine");
			}
			else if (T.VisibleFromCamera(T.GetLimbPosition(nearestPlayer.gameObject.transform, "Right_Hip")))
			{
				vector = T.GetLimbPosition(nearestPlayer.gameObject.transform, "Right_Hip");
			}
			else if (T.VisibleFromCamera(T.GetLimbPosition(nearestPlayer.gameObject.transform, "Left_Foot")))
			{
				vector = T.GetLimbPosition(nearestPlayer.gameObject.transform, "Left_Foot");
			}
			else if (T.VisibleFromCamera(T.GetLimbPosition(nearestPlayer.gameObject.transform, "Right_Leg")))
			{
				vector = T.GetLimbPosition(nearestPlayer.gameObject.transform, "Right_Leg");
			}
			else if (!hkDamageTool.GetPoint(nearestPlayer.gameObject, Player.player.look.aim.position, (double)num, out vector))
			{
				return false;
			}
			ELimb elimb;
			if (G.Settings.AimbotOptions.TargetL == TargetLimb1.RANDOM)
			{
				elimb = T.GetLimb(TargetLimb.RANDOM);
			}
			else
			{
				elimb = (ELimb)G.Settings.AimbotOptions.TargetL;
			}
			TargetLimb targetLimb;
			if (G.Settings.TargetLimb.TryGetValue(nearestPlayer.channel.owner.playerID.steamID.m_SteamID, out targetLimb) && targetLimb != TargetLimb.GLOBAL)
			{
				elimb = T.GetLimb(targetLimb);
			}
			info = new RaycastInfo(nearestPlayer.transform)
			{
				point = vector,
				direction = Player.player.look.aim.forward,
				limb = elimb,
				material = EPhysicsMaterial.NONE,
				player = nearestPlayer
			};
			return true;
		}

		public static bool GetPoint(GameObject Target, Vector3 StartPos, double MaxRange, out Vector3 Point)
		{
			Point = Vector3.zero;
			if (!G.Settings.AimbotOptions.ExpandHitboxes)
			{
				return false;
			}
			if (Target == null)
			{
				return false;
			}
			SphereComponent component = Target.GetComponent<SphereComponent>();
			if (Vector3.Distance(Target.transform.position, StartPos) <= 15.5f)
			{
				Point = Player.player.transform.position;
				return true;
			}
			foreach (Vector3 vector in component.Sphere.GetComponent<MeshCollider>().sharedMesh.vertices)
			{
				Vector3 vector2 = component.Sphere.transform.TransformPoint(vector);
				float num = Vector3.Distance(StartPos, vector2);
				if ((double)num <= MaxRange && !Physics.Raycast(StartPos, Vector3.Normalize(vector2 - StartPos), num, RayMasks.DAMAGE_CLIENT))
				{
					Point = vector2;
					return true;
				}
			}
			return false;
		}
	}
}
