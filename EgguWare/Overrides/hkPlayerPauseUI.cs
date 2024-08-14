using System;
using SDG.Unturned;

namespace EgguWare.Overrides
{
	public class hkPlayerPauseUI
	{
		public static void OV_onClickedExitButton(ISleekElement button)
		{
			Provider.disconnect();
		}
	}
}
