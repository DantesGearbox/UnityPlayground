using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCombo {
	public string horizontalAxis = "HorizontalJ"; 	//Standard PS4: "HorizontalJ"
	public string verticalAxis = "VerticalJ";		//Standard PS4: "VerticalJ"
	public string[] buttons;
	public float diagonalPressure = 0.6384034f; //Defined by Unity. Hold down two d-pad buttons to see.
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
				if ((buttons [currentIndex] == "up" && 
					(Input.GetAxisRaw (horizontalAxis) == 0 && Input.GetAxisRaw (verticalAxis) == -1)) ||
					(buttons [currentIndex] == "down" && 
						(Input.GetAxisRaw (horizontalAxis) == 0 && Input.GetAxisRaw (verticalAxis) == 1)) ||
					(buttons [currentIndex] == "right" && 
						(Input.GetAxisRaw (horizontalAxis) == 1 && Input.GetAxisRaw (verticalAxis) == 0)) ||
					(buttons [currentIndex] == "left" && 
						(Input.GetAxisRaw (horizontalAxis) == -1 && Input.GetAxisRaw (verticalAxis) == 0)) ||
					(buttons [currentIndex] == "down-right" && 
						(Input.GetAxisRaw (horizontalAxis) == diagonalPressure && Input.GetAxisRaw (verticalAxis) == diagonalPressure)) ||
					(buttons [currentIndex] == "down-left" && 
						(Input.GetAxisRaw (horizontalAxis) == -diagonalPressure && Input.GetAxisRaw (verticalAxis) == diagonalPressure)) ||
					(buttons [currentIndex] == "up-right" && 
						(Input.GetAxisRaw (horizontalAxis) == diagonalPressure && Input.GetAxisRaw (verticalAxis) == -diagonalPressure)) ||
					(buttons [currentIndex] == "up-left" && 
						(Input.GetAxisRaw (horizontalAxis) == -diagonalPressure && Input.GetAxisRaw (verticalAxis) == -diagonalPressure)) ||
					(buttons [currentIndex] != "down" && buttons [currentIndex] != "up" && buttons [currentIndex] != "left" && buttons [currentIndex] != "right" &&
						buttons [currentIndex] != "down-right" && buttons [currentIndex] != "down-left" && buttons [currentIndex] != "up-right" && buttons [currentIndex] != "up-left" &&
						Input.GetKeyDown ((KeyCode) System.Enum.Parse (typeof(KeyCode), buttons [currentIndex])))) {

					// Input directions //
					//Up: Hori = 0, Vert -1
					//Down: Hori = 0, Vert 1
					//Right: Hori 1, Vert = 0
					//Left: Hori -1, Vert = 0
					//Down-right: Hori = 0.5, Vert = 0.5
					//Down-left: Hori = -0.5, Vert = 0.5
					//Up-right: Hori = 0.5, Vert = -0.5
					//Up-left: Hori = -0.5, Vert = -0.5 

					Debug.Log (buttons [currentIndex]);
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
