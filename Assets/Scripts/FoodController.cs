using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : SFMonoBehaviour<object> {

	[SerializeField]
	GameObject _foodPrefab;
	public static List<Food> AllFoods = new List<Food> ();

	[SerializeField]
	GameObject _aquarium;

	// Use this for initialization
	void Start () {
		GenerateFoods ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateFoods()
	{
		int foodCount = UnityEngine.Random.Range (100, 200);
		for (int i = 0; i < foodCount; i++) {
			CreateFoodItem ();
		}
	}

	void CreateFoodItem ()
	{
		Food newFood = Instantiate (_foodPrefab).GetComponent<Food>();
		AllFoods.Add (newFood);

		newFood.transform.SetParent (_aquarium.transform);

		// Random position
		int horizBound = (int)(_aquarium.GetComponent<RectTransform> ().rect.width / 2);

		float posX = UnityEngine.Random.Range(-horizBound, horizBound);
		float posY = UnityEngine.Random.Range(_aquarium.GetComponent<RectTransform> ().rect.height / 2,
			_aquarium.GetComponent<RectTransform> ().rect.height / 2 + 200);

		newFood.transform.SetLocalPositionX (posX);
		newFood.transform.SetLocalPositionY (posY);

		newFood.AddEventListener ((int)Food.Events.ON_DESTROY, OnFoodDestroy);
	}

	void OnFoodDestroy(object data)
	{
		Food destroyedFood = (Food)data;
		AllFoods.Remove (destroyedFood);
	}
}
