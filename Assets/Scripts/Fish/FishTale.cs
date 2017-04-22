using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTale : MonoBehaviour {

	bool _moving = true;

	float _deltaRot = 90;

	const float _maxAngle = 45;

	Color _baseColor;

	float _tailColorCoef = 0.2f;

	// Use this for initialization
	void Start () {
		_baseColor = gameObject.GetComponent<UnityEngine.UI.Image> ().color;
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

			UpdateColor ();
		}
	}

	void UpdateColor ()
	{
		float angleY = transform.localRotation.eulerAngles.y;
		float colorValue = (float)(angleY + _maxAngle) / (2 * _maxAngle) * _tailColorCoef;
		gameObject.GetComponent<UnityEngine.UI.Image> ().color = new Color(_baseColor.r - colorValue, _baseColor.g - colorValue, _baseColor.b - colorValue);
	}
}
