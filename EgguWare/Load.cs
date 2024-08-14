using System;
using EgguWare.Utilities;
using SDG.Framework.Modules;
using UnityEngine;

namespace EgguWare
{
	public class Load : IModuleNexus
	{
		public static void Start()
		{
			Load.CO = new GameObject();
			global::UnityEngine.Object.DontDestroyOnLoad(Load.CO);
			Load.CO.AddComponent<Manager>();
		}

		public void initialize()
		{
			Load.Start();
		}

		public void shutdown()
		{
		}

		public static GameObject CO;
	}
}
