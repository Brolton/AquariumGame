using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoving : MonoBehaviour {

	float deltaMove = 50;

	float rightBound = 0;
	float leftBound = 0;

	// Use this for initialization
	void Start () {
		rightBound = transform.parent.GetComponent<RectTransform> ().rect.width / 2 - this.GetComponent<RectTransform> ().rect.width;
		leftBound = -rightBound;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x + deltaMove * Time.deltaTime , transform.localPosition.y, transform.localPosition.z);

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
