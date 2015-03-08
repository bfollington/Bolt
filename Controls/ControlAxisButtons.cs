// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
namespace Control
{
	public class ControlAxisButtons : IControlAxis
	{
		
		private string positive;
		private string negative;
	
		public ControlAxisButtons (string positive, string negative)
		{
			this.positive = positive;
			this.negative = negative;
		}
		
		public float GetAxis()
		{
			var pos = Input.GetButton(positive);
			var neg = Input.GetButton(negative);
		
			if ( pos && !neg )
			{
				return 1f;
			} else if ( neg && !pos)
			{
				return -1f;
			} else {
				return 0f;
			}
		}
	}
}
