using System;
using System.Collections;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Utilities
{
	// Token: 0x0200000E RID: 14
	public class SphereComponent : MonoBehaviour
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003D50 File Offset: 0x00001F50
		private void Awake()
		{
			this.Coroutines.Add(base.StartCoroutine(this.RecalcSize()));
			this.Coroutines.Add(base.StartCoroutine(this.CalcSphere()));
			this.Coroutines.Add(base.StartCoroutine(this.CalcVelocity()));
			base.StartCoroutine(this.Kill());
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002200 File Offset: 0x00000400
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000220F File Offset: 0x0000040F
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

		// Token: 0x06000032 RID: 50 RVA: 0x0000221E File Offset: 0x0000041E
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

		// Token: 0x06000033 RID: 51 RVA: 0x0000222D File Offset: 0x0000042D
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

		// Token: 0x04000012 RID: 18
		public GameObject Sphere;

		// Token: 0x04000013 RID: 19
		public Vector3 LastPos;

		// Token: 0x04000014 RID: 20
		public float Velocity;

		// Token: 0x04000015 RID: 21
		public float SphereRadius;

		// Token: 0x04000016 RID: 22
		public float LastHit;

		// Token: 0x04000017 RID: 23
		public Vector3 SphereScale;

		// Token: 0x04000018 RID: 24
		public List<Coroutine> Coroutines = new List<Coroutine>();
	}
}
