// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Bolt
{
	public class ControlManager
	{

		public const int LEFT_BUTTON = 0;
		public const int RIGHT_BUTTON = 1;
		public const int MIDDLE_BUTTON = 2;
		public const string SCROLL_WHEEL = "SCROLL";
		
		public static readonly KeyCode[] JOYPAD_ONE_BUTTONS = {
			KeyCode.Joystick1Button0,
			KeyCode.Joystick1Button1,
			KeyCode.Joystick1Button2,
			KeyCode.Joystick1Button3,
			KeyCode.Joystick1Button4,
			KeyCode.Joystick1Button5,
			KeyCode.Joystick1Button6,
			KeyCode.Joystick1Button7,
			KeyCode.Joystick1Button8,
			KeyCode.Joystick1Button9
		};
		
		public static readonly string[] JOYPAD_ONE_AXES = {
			"Joy1X"
		};

		public static readonly Dictionary<string, string> xbox = new Dictionary<string, string>
		{
			{ "A", "16" },
			{ "X", "18" },
			{ "B", "17" },
			{ "Y", "19" },
			{ "Back", "9" },
			{ "Start", "10" },
			{ "POVLeft", "7" },
			{ "POVRight", "8" },
			{ "POVUp", "5" },
			{ "POVDown", "6" },
			{ "LB", "13" },
			{ "RB", "14" },
			{ "RS", "12" },
			{ "LS", "11" },
			{ "XB", "15" },
			{ "L_X", "L_X" },
			{ "L_Y", "L_Y" },
			{ "R_X", "R_X" },
			{ "R_Y", "R_Y" },
			{ "RT", "A_6" },
			{ "LT", "A_5" }
		};

		public static readonly Dictionary<string, string> ps3 = new Dictionary<string, string>
		{
			{ "Cross", "14" },
			{ "Triangle", "12" },
			{ "Square", "15" },
			{ "Circle", "13" },
			{ "Select", "0" },
			{ "Start", "3" },
			{ "POVLeft", "7" },
			{ "POVRight", "5" },
			{ "POVUp", "4" },
			{ "POVDown", "6" },
			{ "L1", "10" },
			{ "R1", "11" },
			{ "R3", "2" },
			{ "L3", "1" },
			{ "PS", "16" },
			{ "L_X", "L_X" },
			{ "L_Y", "L_Y" },
			{ "R_X", "R_X" },
			{ "R_Y", "R_Y" },
			{ "R2", "9" },
			{ "L2", "8" }
		};

		public static readonly Dictionary<string, string> ps4 = new Dictionary<string, string>
		{
			{ "Cross", "1" },
			{ "Triangle", "3" },
			{ "Square", "0" },
			{ "Circle", "2" },
			{ "Share", "8" },
			{ "Options", "9" },
			{ "POVH", "A_7" },
			{ "POVV", "A_8" },
			{ "L1", "4" },
			{ "R1", "5" },
			{ "R3", "11" },
			{ "L3", "10" },
			{ "PS", "12" },
			{ "L_X", "L_X" },
			{ "L_Y", "L_Y" },
			{ "R_X", "R_X" },
			{ "R_Y", "R_Y" },
			{ "R2_Press", "7" },
			{ "L2_Press", "6" },
			{ "R2", "A6" },
			{ "L2", "A5" }
		};

		private Dictionary<string, IList<IControlButton>> buttons;
		private Dictionary<string, IList<IControlAxis>> axes;
		private int controllerId;

		public ControlManager(int controllerId)
		{
			this.controllerId = controllerId;
			buttons = new Dictionary<string, IList<IControlButton>>();
			axes = new Dictionary<string, IList<IControlAxis>>();
		}

		public void Update()
		{
			foreach (KeyValuePair<string, IList<IControlButton>> list in buttons)
			{
				foreach (IControlButton b in list.Value)
				{
					b.Update();
				}
			}

			//PrintController();

		}

		public void PrintController()
		{
			if (controllerId > 0)
			{
				for (int i = 0; i <= 20; i++)
				{
					if (Input.GetButton(controllerId + "_" + i))
					{
						Debug.Log (controllerId + "_" + i);
					}
				}
			}

			if (controllerId > 0)
			{
				string output = "";

				output += controllerId + "_" + "L_X: " + Input.GetAxis(controllerId + "_" +"L_X") + ", ";
				output += controllerId + "_" + "L_Y: " + Input.GetAxis(controllerId + "_" +"L_Y") + ", ";
				output += controllerId + "_" + "R_X: " + Input.GetAxis(controllerId + "_" +"R_X") + ", ";
				output += controllerId + "_" + "R_Y: " + Input.GetAxis(controllerId + "_" +"R_Y") + ", ";
				output += controllerId + "_" + "A_5: " + Input.GetAxis(controllerId + "_" +"A_5") + ", ";
				output += controllerId + "_" + "A_6: " + Input.GetAxis(controllerId + "_" +"A_6") + ", ";
				output += controllerId + "_" + "A_7: " + Input.GetAxis(controllerId + "_" +"A_7") + ", ";
				output += controllerId + "_" + "A_8: " + Input.GetAxis(controllerId + "_" +"A_8") + ", ";

				Debug.Log (output);
			}
		}

		public void AddAxisAsAxis(string key, string name)
		{
			AddAxisAsAxis(key, name, false);
		}

		public void AddAxisAsAxis(string key, string name, bool invert)
		{
			if (axes.ContainsKey(key))
			{

			} else {
				axes.Add(key, new List<IControlAxis>());
			}

			axes[key].Add (new ControlAxisAxis(controllerId + "_" + name, invert));
		}

		public void AddButtonsAsAxis(string key, string positive, string negative)
		{
			if (axes.ContainsKey(key))
			{

			} else {
				axes.Add(key, new List<IControlAxis>());
			}

			axes[key].Add (new ControlAxisButtons(controllerId + "_" + positive, controllerId + "_" + negative));
		}

		public void AddButtonAsAxis(string key, string trigger)
		{
			if (axes.ContainsKey(key))
			{

			} else {
				axes.Add(key, new List<IControlAxis>());
			}

			axes[key].Add (new ControlAxisButton(controllerId + "_" + trigger));
		}

		public void AddMouseButtonsAsAxis(string key, int positive, int negative)
		{
			if (axes.ContainsKey(key))
			{

			} else {
				axes.Add(key, new List<IControlAxis>());
			}

			axes[key].Add (new ControlAxisMouseButtons(positive, negative));
		}

		public void AddMouseButtonAsAxis(string key, int index)
		{
			if (axes.ContainsKey(key))
			{

			} else {
				axes.Add(key, new List<IControlAxis>());
			}

			axes[key].Add (new ControlAxisMouseButton(index));
		}

		public Vector3 GetMousePosition()
		{
			var pos = Input.mousePosition;
			return pos;
		}

		public float GetAxis(string key)
		{
			foreach (IControlAxis a in axes[key])
			{
				if (a.GetAxis() != 0)
				{
					return a.GetAxis();
				}
			}

			return 0;
		}

		public void AddButtonAsButton(string key, string name)
		{
			if (buttons.ContainsKey(key))
			{

			} else {
				buttons.Add(key, new List<IControlButton>());
			}

			buttons[key].Add (new ControlButtonButton(controllerId + "_" + name));

		}

		public void AddAxisAsButton(string key, string name, float min, float max)
		{
			if (buttons.ContainsKey(key))
			{

			} else {
				buttons.Add(key, new List<IControlButton>());
			}

			buttons[key].Add (new ControlButtonAxis(controllerId + "_" + name, min, max));

		}

		public void AddMouseButtonAsButton(string key, int index)
		{
			if (buttons.ContainsKey(key))
			{

			} else {
				buttons.Add(key, new List<IControlButton>());
			}

			buttons[key].Add (new ControlButtonMouseButton(index));

		}

		public bool GetButton(string name)
		{
			foreach (IControlButton b in buttons[name])
			{
				if (b.IsDown())
				{
					return true;
				}
			}

			return false;
		}

		public bool JustReleased(string name)
		{
			foreach (IControlButton b in buttons[name])
			{
				if (b.JustReleased())
				{
					return true;
				}
			}

			return false;
		}

		public bool JustPressed(string name)
		{
			foreach (IControlButton b in buttons[name])
			{
				if (b.JustPressed())
				{
					return true;
				}
			}

			return false;
		}
	}
}

