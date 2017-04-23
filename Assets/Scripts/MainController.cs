using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : SFMonoBehaviour<object> {

	enum GameSpeedType {
		GAME_SPEED_NORMAL,
		GAME_SPEED_FAST // 2x
	}

	GameSpeedType _currentGameSpeed = GameSpeedType.GAME_SPEED_NORMAL;

	[SerializeField]
	UnityEngine.UI.Text _clock;
	float startTime = 0;
	int currentDay = 0;

	[SerializeField]
	UnityEngine.UI.Text _scoreLbl;
	float _scrore = 0;

	[SerializeField]
	GameObject _fishPrefab;
	List<Fish> _allFishes = new List<Fish> ();
	int _maxFishesAtOnce = 5;

	[SerializeField]
	GameObject _aquarium;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		GenerateNewFishes ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGameClock ();
		UpdateScore ();
		CheckFishPopulation ();
	}

	void UpdateGameClock() {
		var gameTime = Time.time - startTime;
		TimeSpan timeSpan = TimeSpan.FromSeconds (gameTime);
		_clock.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}

	void UpdateScore() {
		_scoreLbl.text = "Score: " + _scrore.ToString ();
	}

	void  CheckFishPopulation ()
	{
		float gameTime = Time.time - startTime;

		if (currentDay < (int)(gameTime / 10)) // Just for test
		{
			currentDay = (int)(gameTime / 10);
			GenerateNewFishes ();
		}
	}

	void GenerateNewFishes ()
	{
		int newFishesNumber = UnityEngine.Random.Range(1, _maxFishesAtOnce + 1);  // Just for test
		for (int i = 0; i < newFishesNumber; i++) {
			CreateNewFish ();
		}
	}

	void CreateNewFish ()
	{
		Fish newFish = Instantiate (_fishPrefab).GetComponent<Fish>();
		_allFishes.Add (newFish);

		newFish.transform.SetParent (_aquarium.transform);

		// Random position
		int horizBound = (int)(_aquarium.GetComponent<RectTransform> ().rect.width / 2 + newFish.GetComponent<RectTransform> ().rect.width / 2);
		int vertBound = (int)(_aquarium.GetComponent<RectTransform> ().rect.height / 2 - newFish.GetComponent<RectTransform> ().rect.height);

		float posX = (UnityEngine.Random.value > 0.5f) ? horizBound : -horizBound;
		float posY = UnityEngine.Random.Range(-vertBound, vertBound);

		newFish.transform.SetLocalPositionX (posX);
		newFish.transform.SetLocalPositionY (posY);

		newFish.Name = "Fish: " + _allFishes.Count; // Just for test
		newFish.Temperature = UnityEngine.Random.Range(11, 31); // Just for test
		newFish.OxygenPerc = UnityEngine.Random.Range(6, 41); // Just for test
		newFish.RequiredPurity = UnityEngine.Random.Range(1, 36); // Just for test

		newFish.SetColor(Constants.FishColorsList[UnityEngine.Random.Range(0, Constants.FishColorsList.Count)]);

		newFish.AddEventListener ((int)Fish.FishEvents.DEATH, OnFishDead);
	}

	void OnFishDead(object data) {
		Fish deadFish = (Fish)data;
		_allFishes.Remove (deadFish);
		// Calculate scores?
	}
}
