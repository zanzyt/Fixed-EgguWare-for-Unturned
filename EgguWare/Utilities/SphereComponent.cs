using System;
using System.Collections;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{

	public class SphereComponent : MonoBehaviour
	{
		private void Awake()
		{
			this.Coroutines.Add(base.StartCoroutine(this.RecalcSize()));
			this.Coroutines.Add(base.StartCoroutine(this.CalcSphere()));
			this.Coroutines.Add(base.StartCoroutine(this.CalcVelocity()));
			base.StartCoroutine(this.Kill());
		}

		private IEnumerator Kill()
		{
			for (;;)
			{
				yield return new WaitForSeconds(2f);
				if (Time.realtimeSinceStartup - this.LastHit >= 7f)
				{
					foreach (Coroutine coroutine in this.Coroutines)
					{
						base.StopCoroutine(coroutine);
					}
					global::UnityEngine.Object.Destroy(this);
				}
			}
			yield break;
		}

		private IEnumerator RecalcSize()
		{
			for (;;)
			{
				this.SphereRadius = (float)G.Settings.AimbotOptions.HitboxSize;
				global::UnityEngine.Object.Destroy(this.Sphere);
				this.Sphere = CreateICOSphere.Create("HitSphere", this.SphereRadius, (float)G.Settings.AimbotOptions.AimpointMultiplier);
				this.Sphere.layer = 24;
				this.Sphere.transform.parent = base.transform;
				this.Sphere.transform.localPosition = Vector3.zero;
				this.Sphere.transform.localScale = this.SphereScale;
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
		}

		private IEnumerator CalcVelocity()
		{
			for (;;)
			{
				this.LastPos = base.transform.position;
				yield return new WaitForSeconds(0.2f);
				this.Velocity = Vector3.Distance(base.transform.position, this.LastPos) * 5f;
			}
			yield break;
		}

		private IEnumerator CalcSphere()
		{
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				float num = Provider.ping * this.Velocity * 2f;
				float num2 = (this.SphereRadius - num) / this.SphereRadius;
				if (num2 < 0f)
				{
					num2 = 0.05f;
				}
				this.SphereScale = new Vector3(num2, num2, num2);
				if (this.Sphere)
				{
					this.Sphere.transform.localScale = this.SphereScale;
				}
			}
			yield break;
		}

		public GameObject Sphere;

		public Vector3 LastPos;

		public float Velocity;

		public float SphereRadius;

		public float LastHit;

		public Vector3 SphereScale;
		
		public List<Coroutine> Coroutines = new List<Coroutine>();
	}
}
