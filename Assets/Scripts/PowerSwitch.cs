using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerSwitch : MonoBehaviour {
	public GameObject mainController;
	public bool power = true;
	public Sprite imageOn;
	public Sprite imageOff;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			if (AquaCondition.overload) {
				power = false;
			}
			AquaCondition.powerIsOn = power;
		}
		if (power) {
			gameObject.GetComponent<Image> ().sprite = imageOn;
		} else {
			gameObject.GetComponent<Image> ().sprite = imageOff;
		}
	}
	public void OnOff(bool switching){
		switching = true;
		if (switching) {
			power = !power;
		}
	}
}
