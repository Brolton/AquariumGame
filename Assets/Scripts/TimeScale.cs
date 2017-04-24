using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftAlt)) {
			Time.timeScale = 10.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}
}
