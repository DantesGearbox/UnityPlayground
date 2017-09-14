using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanController : MonoBehaviour {

	public float moveDistance = 0.5f;
	public float timeBeforeMove = 0.25f;
	float timeSinceMove = 0.0f;
	enum direction {up, down, left, right, stand};
	public int currentDirection;
	int prevDirection;
	int prevPrevDirection;
	public KeyCode upButton = KeyCode.UpArrow;
	public KeyCode downButton = KeyCode.DownArrow;
	public KeyCode leftButton = KeyCode.LeftArrow;
	public KeyCode rightButton = KeyCode.RightArrow;

	// Use this for initialization
	void Start () {
		currentDirection = (int) direction.stand;
		prevDirection = (int) direction.stand;
		prevPrevDirection = (int) direction.stand;
	}
	
	// Update is called once per frame
	void Update () {

		//Direction has to be synched to movement.
		//KEY DOWN//
		if(Input.GetKeyDown (upButton)){
			prevPrevDirection = prevDirection;
			prevDirection = currentDirection;
			currentDirection = (int) direction.up;
		}
		if(Input.GetKeyDown (downButton)){
			prevPrevDirection = prevDirection;
			prevDirection = currentDirection;
			currentDirection = (int) direction.down;
		}
		if(Input.GetKeyDown (leftButton)){
			prevPrevDirection = prevDirection;
			prevDirection = currentDirection;
			currentDirection = (int) direction.left;
		}
		if(Input.GetKeyDown (rightButton)){
			prevPrevDirection = prevDirection;
			prevDirection = currentDirection;
			currentDirection = (int) direction.right;
		}


		// KEY UP //
		if(Input.GetKeyUp (upButton)){
			if(currentDirection == (int) direction.up){
				currentDirection = prevDirection;
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevDirection == (int) direction.up){
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevPrevDirection == (int) direction.up){
				prevPrevDirection = (int) direction.stand;
			}
		}

		if(Input.GetKeyUp (downButton)){
			if(currentDirection == (int) direction.down){
				currentDirection = prevDirection;
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevDirection == (int) direction.down){
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevPrevDirection == (int) direction.down){
				prevPrevDirection = (int) direction.stand;
			}
		}

		if(Input.GetKeyUp (leftButton)){
			if(currentDirection == (int) direction.left){
				currentDirection = prevDirection;
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevDirection == (int) direction.left){
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevPrevDirection == (int) direction.left){
				prevPrevDirection = (int) direction.stand;
			}
		}

		if(Input.GetKeyUp (rightButton)){
			if(currentDirection == (int) direction.right){
				currentDirection = prevDirection;
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevDirection == (int) direction.right){
				prevDirection = prevPrevDirection;
				prevPrevDirection = (int) direction.stand;
			} else if(prevPrevDirection == (int) direction.right){
				prevPrevDirection = (int) direction.stand;
			}
		}


		timeSinceMove += Time.deltaTime;
		if(timeSinceMove > timeBeforeMove){
			Vector3 moveVector = new Vector3 (0, 0, 0);
			switch (currentDirection){
			case (int) direction.up:
				moveVector = new Vector3 (0, moveDistance, 0);
				break;
			case (int) direction.down:
				moveVector = new Vector3 (0, -moveDistance, 0);
				break;
			case (int) direction.left:
				moveVector = new Vector3 (-moveDistance, 0, 0);
				break;
			case (int) direction.right:
				moveVector = new Vector3 (moveDistance, 0, 0);
				break;
			case (int) direction.stand:
				break;
			}

			transform.Translate (moveVector);
			timeSinceMove = 0;
		}
	}
}
