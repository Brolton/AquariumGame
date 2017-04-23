using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMouth : SFMonoBehaviour<object> {

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

			if (angleY < _minAngle ||
				angleY > _maxAngle) {
				_deltaRot *= -1;
				transform.Rotate(0,0, _deltaRot * Time.deltaTime);
			}

			transform.Rotate(0,0, _deltaRot * Time.deltaTime);
		}
	}

	public void StopMoving() {
		_moving = false;
		transform.Rotate(0,0, 0);
	}

	public void OpenMouth() {
		transform.Rotate(0,0, _maxAngle);
	}
}
