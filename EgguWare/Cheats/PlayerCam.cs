using System;
using EgguWare.Attributes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	// Token: 0x02000047 RID: 71
	[Comp]
	public class PlayerCam : MonoBehaviour
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000A728 File Offset: 0x00008928
		public void Update()
		{
			if (!PlayerCam.cam_obj || !PlayerCam.subCam)
			{
				return;
			}
			if (G.BeingSpied)
			{
				PlayerCam.Enabled = false;
			}
			else
			{
				PlayerCam.Enabled = true;
			}
			if (PlayerCam.Enabled)
			{
				PlayerCam.subCam.enabled = true;
				return;
			}
			PlayerCam.subCam.enabled = false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A784 File Offset: 0x00008984
		private void OnGUI()
		{
			GUI.skin = AssetUtilities.Skin;
			if (G.MainCamera == null)
			{
				G.MainCamera = Camera.main;
			}
			if (PlayerCam.Enabled && PlayerCam.player != null && Provider.isConnected && !G.BeingSpied)
			{
				GUI.color = new Color(1f, 1f, 1f, 0f);
				PlayerCam.viewport = GUI.Window(98, PlayerCam.viewport, new GUI.WindowFunction(this.DoMenu), "Player Cam");
				GUI.color = Color.white;
				if (PlayerCam.IsFullScreen && GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height - 100), 100f, 50f), "End Spectating"))
				{
					PlayerCam.IsFullScreen = false;
				}
			}
			if (PlayerCam.player == null || G.BeingSpied || (!Provider.isConnected && PlayerCam.subCam != null && PlayerCam.cam_obj != null))
			{
				global::UnityEngine.Object.Destroy(PlayerCam.subCam);
				PlayerCam.subCam = null;
				PlayerCam.cam_obj = null;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		private void DoMenu(int windowID)
		{
			if (PlayerCam.cam_obj == null || PlayerCam.subCam == null)
			{
				PlayerCam.cam_obj = new GameObject();
				if (PlayerCam.subCam != null)
				{
					global::UnityEngine.Object.Destroy(PlayerCam.subCam);
				}
				PlayerCam.subCam = PlayerCam.cam_obj.AddComponent<Camera>();
				PlayerCam.subCam.CopyFrom(G.MainCamera);
				PlayerCam.subCam.enabled = true;
				PlayerCam.subCam.rect = new Rect(0.6f, 0.6f, 0.6f, 0.4f);
				PlayerCam.subCam.depth = 98f;
				global::UnityEngine.Object.DontDestroyOnLoad(PlayerCam.cam_obj);
			}
			PlayerCam.subCam.transform.SetPositionAndRotation(T.GetLimbPosition(PlayerCam.player.player.transform, "Skull") + new Vector3(0f, 0.2f, 0f) + PlayerCam.player.player.look.aim.forward / 1.6f, PlayerCam.player.player.look.aim.rotation);
			float num = PlayerCam.viewport.x / (float)Screen.width;
			float num2 = (PlayerCam.viewport.y + 40f) / (float)Screen.height;
			float num3 = PlayerCam.viewport.width / (float)Screen.width;
			float num4 = PlayerCam.viewport.height / (float)Screen.height;
			if (PlayerCam.IsFullScreen)
			{
				num = 0f;
				num2 = 0f;
				num3 = (float)Screen.width;
				num4 = (float)Screen.height;
			}
			num2 = 1f - num2;
			num2 -= num4;
			PlayerCam.subCam.rect = new Rect(num, num2, num3, num4);
			if (!PlayerCam.IsFullScreen)
			{
				GUILayout.Space(-15f);
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Box(PlayerCam.player.playerID.characterName, new GUILayoutOption[] { GUILayout.Height(25f) });
				if (GUILayout.Button("Full-Screen", new GUILayoutOption[] { GUILayout.Height(25f) }))
				{
					PlayerCam.IsFullScreen = true;
				}
				GUILayout.EndHorizontal();
			}
			GUI.DragWindow();
		}

		// Token: 0x04000128 RID: 296
		public static Rect viewport = new Rect((float)(Screen.width - Screen.width / 4 - 10), 30f, (float)(Screen.width / 4), (float)(Screen.height / 4));

		// Token: 0x04000129 RID: 297
		public static GameObject cam_obj;

		// Token: 0x0400012A RID: 298
		public static Camera subCam;

		// Token: 0x0400012B RID: 299
		public static bool WasEnabled;

		// Token: 0x0400012C RID: 300
		public static bool Enabled = true;

		// Token: 0x0400012D RID: 301
		public static SteamPlayer player = null;

		// Token: 0x0400012E RID: 302
		public static bool IsFullScreen = false;
	}
}
