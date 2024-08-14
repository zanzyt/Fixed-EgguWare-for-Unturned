using System;
using EgguWare.Classes;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace EgguWare.Overrides
{
	public class hkChatManager
	{
		public static void OV_receiveChatMessage(CSteamID speakerSteamID, string iconURL, EChatMode mode, Color color, bool isRich, string text)
		{
			Mute mute;
			G.Settings.Mute.TryGetValue(speakerSteamID.m_SteamID, out mute);
			if (mute == Mute.All)
			{
				return;
			}
			if (mute == Mute.Global && mode == EChatMode.GLOBAL)
			{
				return;
			}
			if (mute == Mute.Area && mode == EChatMode.LOCAL)
			{
				return;
			}
			if (mute == Mute.Group && mode == EChatMode.GROUP)
			{
				return;
			}
			text = text.Trim();
			ControlsSettings.formatPluginHotkeysIntoText(ref text);
			if (OptionsSettings.streamer)
			{
				color = Color.white;
			}
			SteamPlayer steamPlayer;
			if (speakerSteamID == CSteamID.Nil)
			{
				steamPlayer = null;
			}
			else
			{
				if (!OptionsSettings.chatText && speakerSteamID != Provider.client)
				{
					return;
				}
				steamPlayer = PlayerTool.getSteamPlayer(speakerSteamID);
			}
			ReceivedChatMessage receivedChatMessage = new ReceivedChatMessage(steamPlayer, iconURL, mode, color, isRich, text);
			ChatManager.receivedChatHistory.Insert(0, receivedChatMessage);
			if (ChatManager.receivedChatHistory.Count > Provider.preferenceData.Chat.History_Length)
			{
				ChatManager.receivedChatHistory.RemoveAt(ChatManager.receivedChatHistory.Count - 1);
			}
			if (ChatManager.onChatMessageReceived != null)
			{
				ChatManager.onChatMessageReceived();
			}
		}
	}
}
