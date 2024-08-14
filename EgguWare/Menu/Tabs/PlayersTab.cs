using System;
using EgguWare.Cheats;
using EgguWare.Classes;
using EgguWare.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace EgguWare.Menu.Tabs
{
	public class PlayersTab
	{
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(10f, 35f, 530f, 400f), "Online Players", "box");
			PlayersTab.scrollPosition1 = GUILayout.BeginScrollView(PlayersTab.scrollPosition1, Array.Empty<GUILayoutOption>());
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				SteamPlayer steamPlayer = Provider.clients[i];
				if (!(steamPlayer.player == Player.player))
				{
					if (PlayersTab.selectedplayer == steamPlayer)
					{
						if (!G.Settings.Priority.ContainsKey(steamPlayer.playerID.steamID.m_SteamID))
						{
							G.Settings.Priority.Add(steamPlayer.playerID.steamID.m_SteamID, Priority.None);
						}
						if (!G.Settings.Mute.ContainsKey(steamPlayer.playerID.steamID.m_SteamID))
						{
							G.Settings.Mute.Add(steamPlayer.playerID.steamID.m_SteamID, Mute.None);
						}
						if (!G.Settings.TargetLimb.ContainsKey(steamPlayer.playerID.steamID.m_SteamID))
						{
							G.Settings.TargetLimb.Add(steamPlayer.playerID.steamID.m_SteamID, TargetLimb.GLOBAL);
						}
						Priority priority;
						G.Settings.Priority.TryGetValue(steamPlayer.playerID.steamID.m_SteamID, out priority);
						Mute mute;
						G.Settings.Mute.TryGetValue(steamPlayer.playerID.steamID.m_SteamID, out mute);
						TargetLimb targetLimb;
						G.Settings.TargetLimb.TryGetValue(steamPlayer.playerID.steamID.m_SteamID, out targetLimb);
						string text = "";
						if (priority == Priority.Friendly)
						{
							text = text + "<color=#" + Colors.ColorToHex(Colors.GetColor("Friendly_Player_ESP")) + ">[FRIENDLY] </color>";
						}
						if (priority == Priority.Marked)
						{
							text = text + "<color=#" + Colors.ColorToHex(Colors.GetColor("Marked_Player_ESP")) + ">[MARKED] </color>";
						}
						if (mute != Mute.None)
						{
							text += "<color=cyan>[MUTED] </color>";
						}
						if (targetLimb != TargetLimb.GLOBAL)
						{
							text += "<color=red>[LIMB] </color>";
						}
						if (PlayerCam.player == steamPlayer)
						{
							text += "<color=cyan>[CAM] </color>";
						}
						if (GUILayout.Button(text + steamPlayer.playerID.characterName, "SelectedButton", Array.Empty<GUILayoutOption>()))
						{
							PlayersTab.selectedplayer = null;
						}
						GUILayout.BeginVertical("SelectedButtonDropdown", Array.Empty<GUILayoutOption>());
						GUILayout.TextField(steamPlayer.playerID.steamID.m_SteamID.ToString(), "label", Array.Empty<GUILayoutOption>());
						if (GUILayout.Button("Limb: " + Enum.GetName(typeof(TargetLimb), targetLimb), Array.Empty<GUILayoutOption>()))
						{
							G.Settings.TargetLimb[steamPlayer.playerID.steamID.m_SteamID] = targetLimb.Next<TargetLimb>();
						}
						if (GUILayout.Button("Mute: " + Enum.GetName(typeof(Mute), mute), Array.Empty<GUILayoutOption>()))
						{
							G.Settings.Mute[steamPlayer.playerID.steamID.m_SteamID] = mute.Next<Mute>();
						}
						if (GUILayout.Button("Status: " + Enum.GetName(typeof(Priority), priority), Array.Empty<GUILayoutOption>()))
						{
							G.Settings.Priority[steamPlayer.playerID.steamID.m_SteamID] = priority.Next<Priority>();
						}
						if (GUILayout.Button("Camera", Array.Empty<GUILayoutOption>()))
						{
							if (PlayerCam.player == steamPlayer)
							{
								PlayerCam.player = null;
							}
							else
							{
								PlayerCam.player = steamPlayer;
							}
						}
						GUILayout.Button("Add Record", Array.Empty<GUILayoutOption>());
						GUILayout.EndVertical();
					}
					else if (GUILayout.Button(steamPlayer.playerID.characterName ?? "", Array.Empty<GUILayoutOption>()))
					{
						PlayersTab.selectedplayer = steamPlayer;
					}
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		public static SteamPlayer selectedplayer = null;

		private static Vector2 scrollPosition1 = new Vector2(0f, 0f);
	}
}
