﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

	public JamController jc;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(jc.transform.position.x, jc.transform.position.y, transform.position.z);
	}
}
