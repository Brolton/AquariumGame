using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : SFMonoBehaviour<object> {

	public enum FishEvents {
		DEATH
	}

	[SerializeField]
	double _health = 100;
	[SerializeField]
	double _hunger = 100;

	string _name = "";
	public string Name
	{
		get {
			return _name;
		}
		set {
			_name = value;
		}
	}

	float _temperature = 0;
	public float Temperature
	{
		get {
			return _temperature;
		}
		set {
			_temperature = value;
		}
	}

	float _oxygenPerc = 0;
	public float OxygenPerc
	{
		get {
			return _oxygenPerc;
		}
		set {
			_oxygenPerc = value;
		}
	}

	float _requiredPurity = 0;
	public float RequiredPurity
	{
		get {
			return _requiredPurity;
		}
		set {
			_requiredPurity = value;
		}
	}

	float deltaHealth = 2.5f;

	[SerializeField]
	FishAI _ai;

	[SerializeField]
	FishViewController _viewController;

	public void SetColor (Color newColor)
	{
		_viewController.Color = newColor;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		DecreaseHunger ();
		CalculateHealth ();
	}

	void DecreaseHunger() {
		_hunger -= deltaHealth * Time.deltaTime;
		if (_hunger < 0) {
			_hunger = 0;
		}
	}

	bool IsHungry() {
		return _hunger == 0;
	}

	bool IsBadTemperature() {
		// TODO
		return false;
	}

	bool IsLowOxygen() {
		// TODO
		return false;
	}

	bool IsLowPurity() {
		// TODO
		return false;
	}

	void CalculateHealth() {
		if (_health <= 0) {
			return;
		}

		if (IsHungry()) {
			_health -= deltaHealth * Time.deltaTime;
		}
		if (IsBadTemperature()) {
			_health -= deltaHealth * Time.deltaTime;
		}
		if (IsLowOxygen()) {
			_health -= deltaHealth * Time.deltaTime;
		}
		if (IsLowPurity()) {
			_health -= deltaHealth * Time.deltaTime;
		}

		if (_health < 0) {
			_health = 0;
			KillSelf ();
		}
	}

	void KillSelf() {
		CallEvent((int)FishEvents.DEATH, this);// Remove from _allFishes in MainController
		_ai.PlayDeathAnimation ();
		_viewController.PlayDeathAnimation ();
		_viewController.AddEventListener ((int)FishViewController.Events.FISH_INVISIBLE, OnFishInvisble);
	}

	void OnFishInvisble(object data) {
		Destroy (this.gameObject);
	}
}
