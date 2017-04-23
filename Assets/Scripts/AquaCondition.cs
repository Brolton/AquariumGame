using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaCondition : MonoBehaviour {
	public bool powerIsOn = true;
	public float currentPower;
	public float fullPower = 250f;
	public float waterTemperature = 10f;
	public float pumpPower = 20f;
	public float oxygenPower = 20f;
	public float heatPower = 20f;
	public float lightPower = 20f;
	public float currentPollution = 0f;
	public float currentOxygenConsuming;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentPower = pumpPower + oxygenPower + heatPower + lightPower;

		if (currentPower > fullPower) {
			powerIsOn = false;
		}
	}
}
