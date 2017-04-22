using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTale : MonoBehaviour {

	bool _moving = true;

	float _deltaRot = 90;

	const float _maxAngle = 45;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_moving) {
			float angleY = transform.localRotation.eulerAngles.y;

			if (Mathf.Abs(angleY) > _maxAngle) {
				_deltaRot *= -1;
				transform.Rotate(0,_deltaRot * Time.deltaTime,0);
			}

			transform.Rotate(0,_deltaRot * Time.deltaTime,0);
		}
	}
}
