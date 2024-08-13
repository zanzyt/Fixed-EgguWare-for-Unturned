using System;
using EgguWare.Attributes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Cheats
{
	[Comp]
	public class Misc : MonoBehaviour
	{
		private void Start()
		{
			Misc.instance = this;
		}

		private void Update()
		{
			if (G.Settings.MiscOptions.FullBright)
			{
				LightingManager.time = 1200U;
			}
			if (Player.player)
			{
				Player.player.look.isOrbiting = G.Settings.MiscOptions.FreeCam;
				Player.player.look.isTracking = G.Settings.MiscOptions.FreeCam;
			}
			if (G.Settings.MiscOptions.Spam && !string.IsNullOrEmpty(G.Settings.MiscOptions.SpamText) && !PlayerLifeUI.chatting)
			{
				ChatManager.sendChat(EChatMode.GLOBAL, G.Settings.MiscOptions.SpamText);
			}
		}

		private void OnGUI()
		{
			if (!G.Settings.MiscOptions.ShowVanishPlayers || G.BeingSpied || !Provider.isConnected || Provider.clients.Count <= 0)
			{
				return;
			}
			GUI.skin = AssetUtilities.Skin;
			Rect rect = GUILayout.Window(7, this.VanishPlayerRect, new GUI.WindowFunction(this.VanishPlayerWindow), "Vanished Players", Array.Empty<GUILayoutOption>());
			this.VanishPlayerRect.x = rect.x;
			this.VanishPlayerRect.y = rect.y;
		}

		private void VanishPlayerWindow(int winid)
		{
			GUILayout.Space(0f);
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (Vector3.Distance(steamPlayer.player.transform.position, Vector3.zero) < 10f)
				{
					GUILayout.Label(steamPlayer.playerID.characterName, Array.Empty<GUILayoutOption>());
				}
			}
			GUI.DragWindow();
		}

		private void FixedUpdate()
		{
			if (Player.player && Provider.isConnected)
			{
				Player player = Player.player;
				InteractableVehicle interactableVehicle;
				if (player == null)
				{
					interactableVehicle = null;
				}
				else
				{
					PlayerMovement movement = player.movement;
					interactableVehicle = ((movement != null) ? movement.getVehicle() : null);
				}
				InteractableVehicle interactableVehicle2 = interactableVehicle;
				if (interactableVehicle2 != null && interactableVehicle2 && Provider.isConnected && !Provider.isLoading)
				{
					Rigidbody component = interactableVehicle2.GetComponent<Rigidbody>();
					if (G.Settings.MiscOptions.VehicleNoClip)
					{
						component.constraints = RigidbodyConstraints.None;
						component.freezeRotation = false;
						component.useGravity = false;
						component.isKinematic = true;
						Transform transform = interactableVehicle2.transform;
						if (Input.GetKey(KeyCode.W))
						{
							component.MovePosition(transform.position + transform.forward * (interactableVehicle2.asset.speedMax * Time.fixedDeltaTime));
						}
						if (Input.GetKey(KeyCode.S))
						{
							component.MovePosition(transform.position - transform.forward * (interactableVehicle2.asset.speedMax * Time.fixedDeltaTime));
						}
						if (Input.GetKey(KeyCode.A))
						{
							transform.Rotate(0f, -2f, 0f);
						}
						if (Input.GetKey(KeyCode.D))
						{
							transform.Rotate(0f, 2f, 0f);
						}
						if (Input.GetKey(KeyCode.UpArrow))
						{
							transform.Rotate(-1.5f, 0f, 0f);
						}
						if (Input.GetKey(KeyCode.DownArrow))
						{
							transform.Rotate(1.5f, 0f, 0f);
						}
						if (Input.GetKey(KeyCode.LeftArrow))
						{
							transform.Rotate(0f, 0f, 1.5f);
						}
						if (Input.GetKey(KeyCode.RightArrow))
						{
							transform.Rotate(0f, 0f, -1.5f);
						}
						if (Input.GetKey(KeyCode.Q))
						{
							transform.position += new Vector3(0f, 0.2f, 0f);
						}
						if (Input.GetKey(KeyCode.E))
						{
							transform.position -= new Vector3(0f, 0.2f, 0f);
							return;
						}
					}
					else
					{
						component.useGravity = true;
						component.isKinematic = false;
					}
				}
			}
		}

		public static Misc instance;

		public Rect VanishPlayerRect = new Rect((float)(Screen.width - 395), 50f, 200f, 100f);
	}
}
