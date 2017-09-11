using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComboContainer : MonoBehaviour {

	private KeyCombo fireballRight = new KeyCombo (new string[] { "down", "down-right", "right", "JoystickButton1" });
	private KeyCombo fireballLeft = new KeyCombo (new string[] { "down", "down-left", "left", "JoystickButton1" });
	private KeyCombo test1 = new KeyCombo (new string[] { "down", "down-left", "right" });
	private KeyCombo test2 = new KeyCombo (new string[] { "JoystickButton1", "JoystickButton1" });
	
	// Update is called once per frame
	void Update ()
	{
		if (fireballRight.Check ()) {
			Debug.Log ("fireball right");
		}
		if (fireballLeft.Check ()) {
			Debug.Log ("fireball left");
		}
		if (test1.Check ()) {
			Debug.Log ("test1 worked");
		}
		if (test2.Check ()) {
			Debug.Log ("test2 worked");
		}
	}


	public class KeyCombo
	{
		public string[] buttons;
		private int currentIndex = 0;
		//moves along the array as buttons are pressed

		public float allowedTimeBetweenButtons = 0.3f;
		//tweak as needed
		private float timeLastButtonPressed;

		public KeyCombo (string[] b)
		{
			buttons = b;
		}

		//usage: call this once a frame. when the combo has been completed, it will return true
		public bool Check ()
		{
			if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons)
				currentIndex = 0;
			{
				if (currentIndex < buttons.Length) {
					if ((buttons [currentIndex] == "down-right" && (Input.GetAxisRaw ("VerticalJ") == -1 && Input.GetAxisRaw ("HorizontalJ") == 1)) ||
					   (buttons [currentIndex] == "down-left" && (Input.GetAxisRaw ("VerticalJ") == -1 && Input.GetAxisRaw ("HorizontalJ") == -1)) ||
					   (buttons [currentIndex] == "up-right" && (Input.GetAxisRaw ("VerticalJ") == 1 && Input.GetAxisRaw ("HorizontalJ") == 1)) ||
					   (buttons [currentIndex] == "up-left" && (Input.GetAxisRaw ("VerticalJ") == 1 && Input.GetAxisRaw ("HorizontalJ") == -1)) ||
					   (buttons [currentIndex] == "down" && Input.GetAxisRaw ("Vertical") == -1) ||
					   (buttons [currentIndex] == "up" && Input.GetAxisRaw ("Vertical") == 1) ||
					   (buttons [currentIndex] == "left" && Input.GetAxisRaw ("Vertical") == -1) ||
					   (buttons [currentIndex] == "right" && Input.GetAxisRaw ("Horizontal") == 1) ||
					   (buttons [currentIndex] != "down" && buttons [currentIndex] != "up" && buttons [currentIndex] != "left" && buttons [currentIndex] != "right" &&
					   buttons [currentIndex] != "down-right" && buttons [currentIndex] != "down-left" && buttons [currentIndex] != "up-right" && buttons [currentIndex] != "up-left" &&
							Input.GetKeyDown (buttons [currentIndex]))) {
						timeLastButtonPressed = Time.time;
						currentIndex++;
					}

					if (currentIndex >= buttons.Length) {
						currentIndex = 0;
						return true;
					} else
						return false;
				}
			}

			return false;
		}
	}
}
