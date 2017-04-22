using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	float deltaMove = 1;

	float rightBound = 280;
	float leftBound = -280;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x + deltaMove , transform.localPosition.y, transform.localPosition.z);

		if (transform.localPosition.x > rightBound) {
			transform.localPosition = new Vector3 (rightBound , transform.localPosition.y, transform.localPosition.z);
			transform.Rotate(0,180,0);
			deltaMove *= -1;
		}

		if (transform.localPosition.x < leftBound) {
			transform.localPosition = new Vector3 (leftBound , transform.localPosition.y, transform.localPosition.z);
			transform.Rotate(0,180,0);
			deltaMove *= -1;
		}
	}
}
