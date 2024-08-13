using System;
using System.Collections;
using SDG.NetPak;
using SDG.NetTransport;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Overrides
{
	// Token: 0x0200001C RID: 28
	public class hkPlayer : MonoBehaviour
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000023CF File Offset: 0x000005CF
		public void OV_ReceiveTakeScreenshot()
		{
			base.StartCoroutine(this.takeScreenshot());
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000023DE File Offset: 0x000005DE
		private IEnumerator takeScreenshot()
		{
			if ((double)(Time.realtimeSinceStartup - hkPlayer.LastSpy) < 0.5 || G.BeingSpied)
			{
				yield break;
			}
			G.BeingSpied = true;
			hkPlayer.LastSpy = Time.realtimeSinceStartup;
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Raw",
				hideFlags = HideFlags.HideAndDontSave
			};
			texture2D.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
			Texture2D texture2D2 = new Texture2D(640, 480, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Final",
				hideFlags = HideFlags.HideAndDontSave
			};
			Color[] pixels = texture2D.GetPixels();
			Color[] array = new Color[texture2D2.width * texture2D2.height];
			float num = (float)texture2D.width / (float)texture2D2.width;
			float num2 = (float)texture2D.height / (float)texture2D2.height;
			for (int i = 0; i < texture2D2.height; i++)
			{
				int num3 = (int)((float)i * num2) * texture2D.width;
				int num4 = i * texture2D2.width;
				for (int j = 0; j < texture2D2.width; j++)
				{
					int num5 = (int)((float)j * num);
					array[num4 + j] = pixels[num3 + num5];
				}
			}
			texture2D2.SetPixels(array);
			byte[] data = texture2D2.EncodeToJPG(33);
			if (data.Length < 30000)
			{
				hkPlayer.SendScreenshotRelay.Invoke(((Player)this).GetNetId(), ENetReliability.Reliable, delegate(NetPakWriter writer)
				{
					ushort num6 = (ushort)data.Length;
					writer.WriteUInt16(num6);
					writer.WriteBytes(data, (int)num6);
				});
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			G.BeingSpied = false;
			yield break;
		}

		// Token: 0x0400003E RID: 62
		public static float LastSpy;

		// Token: 0x0400003F RID: 63
		private static readonly ServerInstanceMethod SendScreenshotRelay = ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay");
	}
}
