using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AquaCondition : SFMonoBehaviour<object> {
	public static bool powerIsOn = true;
	public static bool overload = false;
	public static float currentPower;
	public static float fullPower = 13;

	public static float currentPollution = 0f;
	public static float currentOxygenConsuming;

	const float _minLightLayerAlpha = 127;

	public static AquaCondition Instance = null;

	int _lightPower = 0;
	public int LightPower {
		get {
			return _lightPower;
		}
		set {
			_lightPower = value;
			UpdateLightLayerAlpha ();
		}
	}

	public int heatPower = 0;
	public int filterPower = 0;
	public int oxygenPower = 0;

	[SerializeField]
	public Image _lightLayer;

	// Use this for initialization
	void Start () {
		Instance = this;
		UpdateLightLayerAlpha ();
	}
	
	// Update is called once per frame
	void Update () {
		currentPower = filterPower + oxygenPower + heatPower + LightPower;

		if (currentPower > fullPower) {
			overload = true;
		} else {
			overload = false;
		}
	}

	void UpdateLightLayerAlpha() {
		float newAlpha = _minLightLayerAlpha * (Constants.LIGHT_POWER_STEPS_NUMBER - 1 - _lightPower) / (Constants.LIGHT_POWER_STEPS_NUMBER - 1);
		_lightLayer.color = new Color (_lightLayer.color.r, _lightLayer.color.g, _lightLayer.color.b, newAlpha/255.0f);
	}
}
