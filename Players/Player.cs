using System;
using Bolt;

namespace Bolt
{
	public class Player
	{

		public ControlManager controller;

		public Player ( ControlManager controller )
		{
			this.controller = controller;
		}
	}
}

