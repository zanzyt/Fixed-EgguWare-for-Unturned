using System;
using EgguWare.Attributes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class PlayerCam : MonoBehaviour
	{
	
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

		public static Rect viewport = new Rect((float)(Screen.width - Screen.width / 4 - 10), 30f, (float)(Screen.width / 4), (float)(Screen.height / 4));

		public static GameObject cam_obj;

		public static Camera subCam;

		public static bool WasEnabled;

		public static bool Enabled = true;

		public static SteamPlayer player = null;

		public static bool IsFullScreen = false;
	}
}
