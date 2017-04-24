using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	public GameObject mainController;
	public int lightPower;
	public int counter = 1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			if (AquaCondition.powerIsOn == false) {
				lightPower = 0;
				switch (counter) {
				case 5:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -65));
					break;
				case 4:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -35));
					break;
				case 3:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					break;
				case 2:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 35));
					break;
				default:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 65));
					break;
				}
			} else {
				switch (counter) {
				case 5:
					lightPower = 5;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -65));
					break;
				case 4:
					lightPower = 4;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -35));
					break;
				case 3:
					lightPower = 3;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					break;
				case 2:
					lightPower = 2;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 35));
					break;
				default:
					lightPower = 1;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 65));
					break;
				}
			}
			AquaCondition.lightPower = lightPower;
		}
	}
	void LateUpdate(){
		if (counter > 5) {
			counter = 5;
		} else if (counter < 1) {
			counter = 1;
		}
	}

	public void RotationToRight(bool rightPressed){
		rightPressed = true;
		if (rightPressed == true) {
			counter++;
			//gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 45));
		}
	}
	public void RotationToLeft(bool leftPressed){
		leftPressed = true;
		if (leftPressed == true) {
			counter--;
			//gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 45));
		}
	}
}
