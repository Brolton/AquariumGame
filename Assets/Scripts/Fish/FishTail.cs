using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTail : SFMonoBehaviour<object> {

	bool _moving = true;

	float _deltaRot = 90;

	const float _maxAngle = 45;

	Color _baseColor;

	float _tailColorCoef = 0.4f;

	// Use this for initialization
	void Start () {
		_baseColor = gameObject.GetComponent<UnityEngine.UI.Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (_moving) {
			float angleY = transform.localRotation.eulerAngles.y;

			if (Mathf.Abs(angleY) > _maxAngle) {
				angleY = _maxAngle * Mathf.Abs(angleY) / angleY;
				_deltaRot *= -1;
				transform.Rotate(0,_deltaRot * Time.deltaTime,0);
			}

			transform.Rotate(0,_deltaRot * Time.deltaTime,0);

			float colorMultiply = (angleY + _maxAngle) / (2 * _maxAngle) * _tailColorCoef + (1.0f - _tailColorCoef);
			Color newColor = new Color(_baseColor.r * colorMultiply, _baseColor.g * colorMultiply, _baseColor.b * colorMultiply);
//			gameObject.GetComponent<UnityEngine.UI.Image> ().color = newColor;
		}
	}

	public void StopMoving() {
		_moving = false;
		transform.Rotate(0,0, 0);
	}
}
