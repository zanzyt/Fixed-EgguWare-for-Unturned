using System;
using EgguWare.Utilities;
using SDG.Framework.Modules;
using UnityEngine;

namespace EgguWare
{
	// Token: 0x02000002 RID: 2
	public class Load : IModuleNexus
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		public static void Start()
		{
			Load.CO = new GameObject();
			global::UnityEngine.Object.DontDestroyOnLoad(Load.CO);
			Load.CO.AddComponent<Manager>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002069 File Offset: 0x00000269
		public void initialize()
		{
			Load.Start();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		public void shutdown()
		{
		}

		// Token: 0x04000001 RID: 1
		public static GameObject CO;
	}
}
