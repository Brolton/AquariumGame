using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMouth : MonoBehaviour {

	bool _moving = true;

	float _deltaRot = 70;

	const float _minAngle = 0;
	const float _maxAngle = 45;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (_moving) {
			float angleY = transform.localRotation.eulerAngles.z;

			if (angleY < _minAngle) {
				_deltaRot *= -1;
//				transform.localRotation.eulerAngles.z = 0;
				transform.Rotate(0,0, _deltaRot * Time.deltaTime);
			}

			if (angleY > _maxAngle) {
				_deltaRot *= -1;
				transform.Rotate(0,0, _deltaRot * Time.deltaTime);
			}

			transform.Rotate(0,0, _deltaRot * Time.deltaTime);
		}
	}
}
