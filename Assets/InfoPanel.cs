using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text _nameLbl;

	[SerializeField]
	Image _lifeBar;

	[SerializeField]
	UnityEngine.UI.Text _heatLbl;

	[SerializeField]
	UnityEngine.UI.Text _lightLbl;


	public static InfoPanel Instance;

	float _startNewLife;

	float _maxLifeTime = 10;

	Fish _targetFish = null;

	// Use this for initialization
	void Start () {
		Instance = this;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		float currentTIme = Time.time;
		if (currentTIme - _startNewLife < _maxLifeTime &&
		    _targetFish != null) {
			UpdateInfo ();
			this.transform.localPosition = _targetFish.transform.localPosition;
		} else {
			gameObject.SetActive (false);
		}
	}

	public static void ShowInfo(Fish someFish)
	{
		Instance.UpdateSelf (someFish);
	}

	void UpdateSelf(Fish someFish)
	{
		gameObject.SetActive (true);
				
		if (_targetFish != null) {
			_targetFish.RemoveEventListener ((int)Fish.FishEvents.DEATH, OnFishDead);
		}

		_targetFish = someFish;
		_targetFish.AddEventListener ((int)Fish.FishEvents.DEATH, OnFishDead);

		this.transform.localPosition = someFish.transform.localPosition;

		_startNewLife = Time.time;
	}

	void UpdateInfo()
	{
		_nameLbl.text = "Name: " + _targetFish.Name;
		_lifeBar.fillAmount = _targetFish.Health / 100;
		_heatLbl.text = "Req Temperature: " + _targetFish.TempRequired;
		_lightLbl.text = "Req Light: " + _targetFish.LightRequired;
	}

	void OnFishDead(object data)
	{
		if (_targetFish != null) {
			_targetFish.RemoveEventListener ((int)Fish.FishEvents.DEATH, OnFishDead);
		}
		_targetFish = null;
	}
}
