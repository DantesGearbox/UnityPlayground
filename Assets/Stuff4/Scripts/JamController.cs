using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamController : MonoBehaviour {

	private RaycastController rc;
	private Rigidbody2D rb;
	private bool jumpPressed = false;
	private bool jumpLetGo = false;
	public Vector2 jumpVector = new Vector2 (0, 40); 
	public Vector2 airJumpVector = new Vector2 (0, 40);
	public KeyCode jumpKey = KeyCode.Space; //Default PS4: KeyCode.JoystickButton1
	public string horizontalAxis = "Horizontal"; //Default PS4: "HorizontalJ"
	public string verticalAxis = "Vertical"; //Default PS4: "VerticalJ"
	public int minJumpVelocity = 10;
	private float directionalInput;
	public int moveSpeed = 12;
	private bool inAir = false;
	public int airJumps = 1;
	public int airSpecials = 1;
	public Vector2 specialJumpVectorRight = new Vector2 (40, 30);
	public Vector2 specialJumpVectorLeft = new Vector2 (-40, 30);
	public float specialMovespeedSlowdown = 1.0f;

	private bool doingSpecialMoveLeft = false;
	private bool doingSpecialMoveRight = false;

	private KeyCombo fireballRight = new KeyCombo (new string[] { "down", "down-right", "right", "JoystickButton1" });
	private KeyCombo fireballLeft = new KeyCombo (new string[] { "down", "down-left", "left", "JoystickButton1" });
	/*
	private KeyCombo test1 = new KeyCombo (new string[] { "JoystickButton1", "JoystickButton1" });
	private KeyCombo test2 = new KeyCombo (new string[] { "up" });
	private KeyCombo test3 = new KeyCombo (new string[] { "down" });
	private KeyCombo test4 = new KeyCombo (new string[] { "left" });
	private KeyCombo test5 = new KeyCombo (new string[] { "right" });
	private KeyCombo test6 = new KeyCombo (new string[] { "down-right" });
	private KeyCombo test7 = new KeyCombo (new string[] { "down-left" });
	private KeyCombo test8 = new KeyCombo (new string[] { "up-right" });
	private KeyCombo test9 = new KeyCombo (new string[] { "up-left" });
	*/

	// Use this for initialization
	void Start () {
		rc = GetComponent<RaycastController> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		inAir = false;
		airJumps = 1;
		airSpecials = 1;
	}

	void OnCollisionExit2D(Collision2D coll){
		inAir = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (fireballRight.Check ()) {
			Debug.Log ("fireball right");
			if(airSpecials > 0){
				doingSpecialMoveRight = true;	
				airSpecials--;
			}
		}
		if (fireballLeft.Check ()) {
			Debug.Log ("fireball left");
			if(airSpecials > 0) {
				doingSpecialMoveLeft = true;
				airSpecials--;
			}
		}

		//Dont jump and stuff like that while we are special-moving
		if(doingSpecialMoveLeft || doingSpecialMoveRight){
			return;
		}

		/*
		Debug.Log ("VerticalAxis: " + Input.GetAxisRaw (verticalAxis));
		Debug.Log ("HorizontalAxis: " + Input.GetAxisRaw (horizontalAxis));
		test2.Check ();
		test3.Check ();
		test4.Check ();
		test5.Check ();
		test6.Check ();
		test7.Check ();
		test8.Check ();
		test9.Check ();
		*/

		if(Input.GetKeyDown (jumpKey)){
			jumpPressed = true;
		}

		if(Input.GetKeyUp (jumpKey)){
			jumpLetGo = true;
		}

		directionalInput = Input.GetAxisRaw (horizontalAxis);

	}

	//Also called at a specific interval
	void FixedUpdate(){

		//We only want to influence the horizontal speed if we are going faster than our movespeed.
		float targetVelocityX = directionalInput * moveSpeed;
		if(rb.velocity.x >= -12 && rb.velocity.x <= 12){
			rb.velocity = new Vector2 (targetVelocityX , rb.velocity.y);	
		} else {
			// If targetVelocity (user input) is going opposite of speed, slow down by set amount.
			if(Mathf.Sign (targetVelocityX) != Mathf.Sign (rb.velocity.x)){
				rb.velocity = new Vector2 (rb.velocity.x + specialMovespeedSlowdown * Mathf.Sign (targetVelocityX) , rb.velocity.y);
			}
		}

		if(doingSpecialMoveLeft){
			rb.velocity = Vector2.zero;
			rb.velocity += specialJumpVectorLeft;
			doingSpecialMoveLeft = false;
		}

		if(doingSpecialMoveRight){
			rb.velocity = Vector2.zero;
			rb.velocity += specialJumpVectorRight;
			doingSpecialMoveRight = false;
		}

		if(jumpPressed){
			if(!inAir){
				rb.velocity += jumpVector;
			} else if(inAir && airJumps > 0){
				rb.velocity = Vector2.zero;
				rb.velocity += airJumpVector;
				airJumps--;
			}
			jumpPressed = false;
		}

		if(jumpLetGo){
			if(rb.velocity.y > minJumpVelocity){
				rb.velocity = new Vector2(rb.velocity.x, minJumpVelocity);
			}
			jumpLetGo = false;
		}
	}
}
