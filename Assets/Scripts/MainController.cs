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

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGameClock ();
	}

	void UpdateGameClock() {
		var guiTime = Time.time - startTime;

		TimeSpan timeSpan = TimeSpan.FromSeconds (guiTime);
		_clock.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}
}
