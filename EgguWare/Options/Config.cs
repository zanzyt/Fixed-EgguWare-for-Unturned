using System;
using System.Collections.Generic;
using EgguWare.Classes;
using EgguWare.Options.ESP;

namespace EgguWare.Options
{
	public class Config
	{
		public ESPOptions BedOptions = new ESPOptions();

		public ESPOptions PlayerOptions = new ESPOptions();

		public ESPOptions ItemOptions = new ESPOptions();

		public ESPOptions StorageOptions = new ESPOptions();

		public ESPOptions VehicleOptions = new ESPOptions();

		public ESPOptions ZombieOptions = new ESPOptions();

		public ESPOptions FlagOptions = new ESPOptions();

		public OtherOptions GlobalOptions = new OtherOptions();

		public AimbotOptions AimbotOptions = new AimbotOptions();

		public WeaponOptions WeaponOptions = new WeaponOptions();

		public MiscOptions MiscOptions = new MiscOptions();

		public Dictionary<ulong, Priority> Priority = new Dictionary<ulong, Priority>();

		public Dictionary<ulong, TargetLimb> TargetLimb = new Dictionary<ulong, TargetLimb>();

		public Dictionary<ulong, Mute> Mute = new Dictionary<ulong, Mute>();
	}
}
