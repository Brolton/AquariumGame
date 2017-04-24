using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
	public static List<Color> FishColorsList = new List<Color> {
		new Color(255/255.0f, 204/255.0f, 0/255.0f),
		new Color(255/255.0f, 0/255.0f, 0/255.0f),
		new Color(255/255.0f, 102/255.0f, 0/255.0f),
		new Color(153/255.0f, 204/255.0f, 255/255.0f),
		new Color(0/255.0f, 204/255.0f, 204/255.0f),
		new Color(51/255.0f, 255/255.0f, 255/255.0f),
		new Color(0/255.0f, 153/255.0f, 51/255.0f),
		new Color(0/255.0f, 255/255.0f, 153/255.0f)
	};

	public static Color DeathColor = new Color(153/255.0f, 153/255.0f, 153/255.0f);


	public const int LIGHT_POWER_STEPS_NUMBER = 6; // From 0 to 5
	public const int TEMP_POWER_STEPS_NUMBER = 6;
	public const int POLL_POWER_STEPS_NUMBER = 6;
	public const int AIR_POWER_STEPS_NUMBER = 6;

	// TUNING
//	public const float DiscomfortMul = 1.0f;
//	public const float DiscomfortIgnoreLevel = 2.0f;
//	public const float PollutionMul = 0.5f;
//	public const float AirDeficitMul = 0.5f;
	public const float DiscomfortMul = 0.1f;
	public const float DiscomfortIgnoreLevel = 0.2f;
	public const float PollutionMul = 0.05f;
	public const float AirDeficitMul = 0.05f;

	public const float AQUA_MAX_POLLUTION = 100.0f;
	public const float FISH_POL_NEWBORN = 2.5f;
	public const float FISH_POL_CHILD = 3.5f;
	public const float FISH_POL_ADULT = 5.0f;

	public const float AQUA_MAX_OXYGEN = 100.0f;
}
