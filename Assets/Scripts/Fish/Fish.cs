using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour {

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
	Image _body;
	[SerializeField]
	Image _mouth;
	[SerializeField]
	Image _tail;

	Color _color = new Color();
	public Color Color
	{
		get {
			return _color;
		}
		set {
			_color = value;
			_body.color = _color;
			_mouth.color = _color;
			_tail.color = _color;
		}
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
			Destroy (this.gameObject);
		}


		// Remove from _allFishes
	}
}
