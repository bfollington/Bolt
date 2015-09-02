using System;

namespace Bolt
{
	public interface IControlButton
	{

		bool IsDown();
		bool JustPressed();
		bool JustReleased();
		void Update();

	}
}

