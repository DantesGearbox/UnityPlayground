using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController12 : MonoBehaviour {

	//Build 1, cancels
	//Jump, quite high
	//Press jump again to dive
	//If you land while diving you create a shockwave that might blast off opponents, but you also stand still and "recharge"
	//Cancel a dive mid-air by pressing pressing space while diving

	//Build 2, multiple jumps
	//Jump, "normal" height
	//Press jump again to do another jump; probably can't be too powerful to minimize downtime
	//Double press jump/press another button to dive
	//Dive and hit the ground to create shockwave that can knock opponent off platform
	
	//public Vector2 velocity;
	private Transform tr;

	public bool inAir, diving, stunned, diveCancellable;
	int stunCounter = 0;
	int divingCounter = 0;
	public int stunDuration = 20;
	public int diveCancelDuration = 5;

	public WaveController1 wave;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter (Collision col){
		Debug.Log ("CollisionEnter");
	}

	void OnTriggerEnter (Collider col){
		Debug.Log ("TriggerEnter");
	}

	void Update () {
		//-3.38 ~ is ground
		if(tr.position.y < -3.30){
			inAir = false;
			divingCounter = 0;
			diveCancellable = false;
		} else {
			inAir = true;
		}

		//Hit ground while diving - create a shockwave
		if(diving && !inAir){
			stunned = true;
			diving = false;

			Instantiate (wave, tr.position + new Vector3(-2, 0, 0), tr.rotation);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && !inAir && !stunned) {
			rb.velocity = new Vector2 (0, 12);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && inAir) {
			diving = true;
			rb.velocity = new Vector2 (0, -12);
		}

		if(diving){
			divingCounter++;
			if(divingCounter > diveCancelDuration){
				diveCancellable = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && diving && diveCancellable) {
			diving = false;
			diveCancellable = false;
			divingCounter = 0;
			rb.velocity = new Vector2 (0, 0);
		}

		if(stunned){
			stunCounter++;
			if(stunCounter > stunDuration){
				stunned = false;
				stunCounter = 0;
			}
		}
	}

	void FixedUpdate(){
		//tr.Translate (velocity);
	}
}
