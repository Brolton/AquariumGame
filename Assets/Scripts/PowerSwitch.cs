using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerSwitch : MonoBehaviour {
	public GameObject mainController;
	public bool power = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			if (mainController.GetComponent<AquaCondition> ().overload) {
				power = false;
			}
			mainController.GetComponent<AquaCondition> ().powerIsOn = power;
		}
		if (power) {
			gameObject.GetComponent<Image> ().color = new Color (0, 0, 255);
		} else {
			gameObject.GetComponent<Image> ().color = new Color (255, 0, 0);
		}
	}
	public void OnOff(bool switching){
		switching = true;
		if (switching) {
			power = !power;
		}
	}
}
