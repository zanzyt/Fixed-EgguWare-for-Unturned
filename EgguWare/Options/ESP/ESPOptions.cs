using System;
using EgguWare.Classes;

namespace EgguWare.Options.ESP
{
	public class ESPOptions
	{
		public bool Enabled;

		public bool Glow = true;

		public bool Box = true;

		public bool Distance = true;

		public bool Name = true;

		public bool Tracers = true;

		public int MaxDistance = 400;

		public int FontSize = 11;

		public ShaderType ChamType;
	}
}
