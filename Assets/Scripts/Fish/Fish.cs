using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : SFMonoBehaviour<object> {

	public enum FishEvents {
		DEATH,
		BORN_NEW_FISH
	}

	public enum FishSizes {
		NEWBORN,
		CHILD,
		ADULT
	}

	public FishSizes CurrentSize = FishSizes.NEWBORN;
	int _currentDay = 0;

	float _startLifeTime = 0;

	[SerializeField]
	float _health = 100;
	public float Health {
		get {
			return _health;
		}
	}
	public void AddHealth(float delta) {
		_health += delta;
		_health = _health > 100 ? 100 : _health;
	}

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

	[SerializeField]
	public int LightRequired = 0;

	[SerializeField]
	public int TempRequired = 0;

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

	public Color GetColor ()
	{
		return _viewController.Color;
	}

	// Use this for initialization
	void Start () {
		_startLifeTime = Time.time;
		InvokeRepeating("CalculateHealth", 0, Constants.CALCULATE_HEALTH_PERIOD);
	}
	
	// Update is called once per frame
	void Update () {
		DecreaseHunger ();
//		CalculateHealth ();
		CheckFishAge ();
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

//	void CalculateHealth() {
//		if (_health <= 0) {
//			return;
//		}
//
//		if (IsHungry()) {
//			_health -= deltaHealth * Time.deltaTime;
//		}
//		if (IsBadTemperature()) {
//			_health -= deltaHealth * Time.deltaTime;
//		}
//		if (IsLowOxygen()) {
//			_health -= deltaHealth * Time.deltaTime;
//		}
//		if (IsLowPurity()) {
//			_health -= deltaHealth * Time.deltaTime;
//		}
//
//		if (_health < 0) {
//			_health = 0;
//			KillSelf ();
//		}
//	}

	// Anton's version
	void CalculateHealth()
	{
//		int fishLifeTime = (int)(Time.time - _startLifeTime);
//		if (fishLifeTime <= 0 ||
//			fishLifeTime % Constants.CALCULATE_HEALTH_PERIOD != 0) {
//			return;
//		}

		int lightDiscomfort = Mathf.Abs (LightRequired - AquaCondition.Instance.LightPower);

		int tempDiscomfort = Mathf.Abs (TempRequired - AquaCondition.Instance. HeatPower);

		float discomfort = ((lightDiscomfort + tempDiscomfort) - Constants.DiscomfortIgnoreLevel) * Constants.DiscomfortMul;
		discomfort = discomfort > 0 ? discomfort : 0;

		float pollution = (MainController.FishPollutionTotal - AquaCondition.Instance.filterPower * Constants.AQUA_MAX_POLLUTION / (Constants.POLL_POWER_STEPS_NUMBER - 1)) * Constants.PollutionMul;
		pollution = pollution > 0 ? pollution : 0;

		float airDeficit = (MainController.FishAirConsumeTotal - AquaCondition.Instance.oxygenPower * Constants.AQUA_MAX_OXYGEN / (Constants.AIR_POWER_STEPS_NUMBER - 1)) * Constants.AirDeficitMul;
		airDeficit = airDeficit > 0 ? airDeficit : 0;

		float fishHealthLoss = discomfort + pollution + airDeficit;
		_health -= fishHealthLoss;

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

	void  CheckFishAge ()
	{
		float fishLifeTime = Time.time - _startLifeTime;

		if (_currentDay < (int)(fishLifeTime / 30)) // Just for test
		{
			_currentDay = (int)(fishLifeTime / 30);
			if (CurrentSize != FishSizes.ADULT) {
				CurrentSize = (FishSizes)((int)CurrentSize + 1);
				_viewController.Grow (CurrentSize);
			} else {
				BornNewFish ();
			}
		}
	}

	void BornNewFish ()
	{
		CallEvent ((int)FishEvents.BORN_NEW_FISH, this);
	}

	public void OnMouseDown()
	{
		if (_health > 0) {
			InfoPanel.ShowInfo (this);
		}
	}
}
