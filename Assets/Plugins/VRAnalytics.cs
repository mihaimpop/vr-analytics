#if !UNITY_WEBPLAYER
#define USE_FileIO
#endif
using UnityEngine;
using System.Collections;

namespace VRAnalytics
{
	public class ResourceNode
	{
		public float influence;
		public string name;
		public string color;

		public ResourceNode(float _influence, string _name, string _color)
		{
			influence = _influence;
			name = _name;
			color = _color;
		}
	}

}
