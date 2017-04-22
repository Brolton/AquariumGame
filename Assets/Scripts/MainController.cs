using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

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
		System.Random rnd = new System.Random();
		int newFishesNumber = rnd.Next(1, _maxFishesAtOnce + 1);  // Just for test
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
		System.Random rnd = new System.Random();
		int horizBound = (int)(_aquarium.GetComponent<RectTransform> ().rect.width / 2 - newFish.GetComponent<RectTransform> ().rect.width);
		int vertBound = (int)(_aquarium.GetComponent<RectTransform> ().rect.height / 2 - newFish.GetComponent<RectTransform> ().rect.height);
		newFish.transform.SetLocalPositionX (rnd.Next (-horizBound, horizBound));
		newFish.transform.SetLocalPositionY (rnd.Next (-vertBound, vertBound));

		newFish.Name = "Fish: " + _allFishes.Count; // Just for test
		newFish.Temperature = rnd.Next(11, 31); // Just for test
		newFish.OxygenPerc = rnd.Next(6, 41); // Just for test
		newFish.RequiredPurity = rnd.Next(1, 36); // Just for test
	}
}
