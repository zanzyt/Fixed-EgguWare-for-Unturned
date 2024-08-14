using System;
using EgguWare.Options.ESP;
using UnityEngine;

namespace EgguWare.Classes
{

	public class ESPObj
	{
		public ESPObj(ESPObject t, object o, GameObject go, ESPOptions opt)
		{
			this.Target = t;
			this.Object = o;
			this.GObject = go;
			this.Options = opt;
		}


		public ESPObject Target;
		
		public object Object;

		public GameObject GObject;

		public ESPOptions Options;
	}
}
