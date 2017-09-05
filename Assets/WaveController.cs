using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	Transform tr;
	public int waveSpeed = 5;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		tr.Translate (new Vector3(waveSpeed, 0, 0) * Time.deltaTime);
	}
}
