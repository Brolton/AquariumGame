using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AquaCondition : SFMonoBehaviour<object> {
	public static bool powerIsOn = true;
	public static bool overload = false;
	public static float currentPower;
	public static float fullPower = 20;

	public static float currentPollution = 0f;
	public static float currentOxygenConsuming;

	const float _minLightLayerAlpha = 127;

	public static AquaCondition Instance = null;

	[SerializeField]
	public Image _lightLayer;
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
		
	[SerializeField]
	public Image _termometer;
	int _heatPower = 0;
	public int HeatPower {
		get {
			return _heatPower;
		}
		set {
			_heatPower = value;
			UpdateTermometer ();
		}
	}

	public int filterPower = 5;
	public int oxygenPower = 5;

	// Use this for initialization
	void Start () {
		Instance = this;
		UpdateLightLayerAlpha ();
	}
	
	// Update is called once per frame
	void Update () {
		currentPower = filterPower + oxygenPower +  HeatPower + LightPower;

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

	void UpdateTermometer() {
		_termometer.fillAmount = (float)_heatPower / (float)(Constants.TEMP_POWER_STEPS_NUMBER - 1);
	}
}
