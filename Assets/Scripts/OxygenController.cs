﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour {
	public GameObject mainController;
	public float oxygenPower;
	public int counter = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			if (mainController.GetComponent<AquaCondition> ().powerIsOn == false) {
				oxygenPower = 0f;
				switch (counter) {
				case 5:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -40));
					break;
				case 4:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -20));
					break;
				case 3:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					break;
				case 2:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 20));
					break;
				default:
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 40));
					break;
				}
			} else {
				switch (counter) {
				case 5:
					oxygenPower = 100f;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -40));
					break;
				case 4:
					oxygenPower = 80f;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -20));
					break;
				case 3:
					oxygenPower = 60f;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					break;
				case 2:
					oxygenPower = 40f;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 20));
					break;
				default:
					oxygenPower = 20f;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 40));
					break;
				}
			}
			mainController.GetComponent<AquaCondition> ().oxygenPower = oxygenPower;
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
