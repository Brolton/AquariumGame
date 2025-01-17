﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatController : MonoBehaviour {
	public GameObject mainController;
	public int heatPower;
	public int counter = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			if (AquaCondition.powerIsOn == false) {
				heatPower = 0;
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
					heatPower = 5;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -65));
					break;
				case 4:
					heatPower = 4;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, -35));
					break;
				case 3:
					heatPower = 3;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					break;
				case 2:
					heatPower = 2;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 35));
					break;
				default:
					heatPower = 1;
					gameObject.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (new Vector3 (0, 0, 65));
					break;
				}
			}
			AquaCondition.Instance. HeatPower = heatPower;
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
