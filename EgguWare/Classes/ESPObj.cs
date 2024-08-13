using System;
using EgguWare.Options.ESP;
using UnityEngine;

namespace EgguWare.Classes
{
	// Token: 0x0200003D RID: 61
	public class ESPObj
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000258A File Offset: 0x0000078A
		public ESPObj(ESPObject t, object o, GameObject go, ESPOptions opt)
		{
			this.Target = t;
			this.Object = o;
			this.GObject = go;
			this.Options = opt;
		}

		// Token: 0x04000108 RID: 264
		public ESPObject Target;

		// Token: 0x04000109 RID: 265
		public object Object;

		// Token: 0x0400010A RID: 266
		public GameObject GObject;

		// Token: 0x0400010B RID: 267
		public ESPOptions Options;
	}
}
