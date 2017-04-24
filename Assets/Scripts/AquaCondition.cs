using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaCondition : SFMonoBehaviour<object> {
	public static bool powerIsOn = true;
	public static bool overload = false;
	public static float currentPower;
	public static float fullPower = 250f;

	public static float currentPollution = 0f;
	public static float currentOxygenConsuming;

	public static int lightPower = 1;
	public static int heatPower = 1;
	public static int filterPower = 1;
	public static int oxygenPower = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentPower = filterPower + oxygenPower + heatPower + lightPower;

		if (currentPower > fullPower) {
			overload = true;
		} else {
			overload = false;
		}
	}
}
