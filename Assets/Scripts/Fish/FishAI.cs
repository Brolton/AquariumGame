using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : SFMonoBehaviour<object>
{
	bool _aiIsActive = true;
	bool _isDead = false;

	float deltaMove = 50;
	float rightBound = 0;
	float topBound = 0;

	[SerializeField]
	FishMouth _mouth;
	[SerializeField]
	FishTail _tail;

	[SerializeField]
	int _fishSpeed = 50; // for tests

	const int _fastSpeed = 100; // go to food

	[SerializeField]
	Vector3 _targetPos;

	[SerializeField]
	Food _targetFood;

	float _maxFishAngle = 45;

	float _maxDistanceToFood = 300;

	// Use this for initialization
	void Start () {
		rightBound = transform.parent.GetComponent<RectTransform> ().rect.width / 2 + this.GetComponent<RectTransform> ().rect.width / 4;
		topBound = transform.parent.GetComponent<RectTransform> ().rect.height / 2 - this.GetComponent<RectTransform> ().rect.height;

		GenetateNewTargetPosAndSpeed ();
	}

	// Update is called once per frame
	void Update () {
		if (_aiIsActive) {
			if (_targetFood != null || IsHungryAndFoodNearby ()) {
				SwimToFood ();
			} else {
				JustSwim ();
			}
		}
		if (_isDead) {
			MoveTop ();
		}
	}

	void MoveTop ()
	{
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y + deltaMove * Time.deltaTime, transform.localPosition.z);
	}

	public void PlayDeathAnimation() {
		_aiIsActive = false;
		_isDead = true;
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
		_tail.StopMoving();
		_mouth.StopMoving();
	}

	void JustSwim ()
	{
		if ( Vector3.SqrMagnitude(transform.localPosition - _targetPos) > 0.0001 ) {
			ChangeFishDirectionIfNeed (_targetPos);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, _targetPos, Time.deltaTime * _fishSpeed);
		} else {
			GenetateNewTargetPosAndSpeed ();
		}
		
//		transform.localPosition = new Vector3 (transform.localPosition.x + deltaMove * Time.deltaTime , transform.localPosition.y, transform.localPosition.z);
//
//		if (transform.localPosition.x > rightBound) {
//			transform.localPosition = new Vector3 (rightBound , transform.localPosition.y, transform.localPosition.z);
//			transform.Rotate(0,180,0);
//			deltaMove *= -1;
//		}
//
//		if (transform.localPosition.x < leftBound) {
//			transform.localPosition = new Vector3 (leftBound , transform.localPosition.y, transform.localPosition.z);
//			transform.Rotate(0,180,0);
//			deltaMove *= -1;
//		}
	}

	void GenetateNewTargetPosAndSpeed() {
		_targetPos = new Vector3 (UnityEngine.Random.Range(-(int)rightBound, (int)rightBound), UnityEngine.Random.Range(-(int)topBound, (int)topBound));
		ChangeFishDirectionIfNeed (_targetPos);

		Vector3 heading = _targetPos - transform.localPosition;
		float distance = heading.magnitude;

		_fishSpeed = UnityEngine.Random.Range (_fastSpeed/4, _fastSpeed/2 + 1);
	}

	void ChangeFishDirectionIfNeed(Vector3 neededPos) {

		// check need direction
		float width = GetComponent<RectTransform> ().rect.width;
		if (neededPos.x - transform.localPosition.x < 0 &&
			transform.rotation.eulerAngles.y != 0 
			&& Mathf.Abs(neededPos.x - transform.localPosition.x) >  width/4
		) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
		}
		if (neededPos.x - transform.localPosition.x > 0 &&
			transform.rotation.eulerAngles.y != 180
			&& Mathf.Abs(neededPos.x - transform.localPosition.x) >  width/4
		) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
		}
			
		var vectorToTarget = neededPos - transform.localPosition;
		vectorToTarget.Normalize ();

		float rot_z = -Mathf.Atan2(vectorToTarget.y, Mathf.Abs(vectorToTarget.x)) * Mathf.Rad2Deg;
		if (rot_z < -_maxFishAngle) {
			rot_z = -_maxFishAngle;
		}
		if (rot_z >_maxFishAngle) {
			rot_z = _maxFishAngle;
		}

		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rot_z);

//		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rot_z - 90);

//		var heading = _targetPos - transform.localPosition;
//		var distance = heading.magnitude;
//		var direction = heading / distance; // This is now the normalized direction.
////		transform.localRotation.SetEulerAngles(direction);
//
//		var q = Quaternion.LookRotation(heading);
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100 * Time.deltaTime);
	}

	bool IsHungryAndFoodNearby() {
		if (FoodController.AllFoods.Count == 0) {
			return false;
		}

		int minDistance = int.MaxValue;

		foreach (Food someFood in FoodController.AllFoods) {
			if (someFood.hunterFish != null) {
				continue;
			}
			var heading = someFood.transform.localPosition - transform.localPosition;
			var distance = heading.magnitude;
			if (distance < _maxDistanceToFood &&
				distance < minDistance) {
				minDistance = (int)distance;
				_targetFood = someFood;
			}
		}

		if (_targetFood != null) {
			_targetFood.hunterFish = this.GetComponent<Fish> ();
			_targetFood.AddEventListener ((int)Food.Events.ON_DESTROY, OnFoodDestroy);
		}
		return (_targetFood != null);
	}

	void OnFoodDestroy(object data) {
		_targetFood = null;
	}

	void SwimToFood()
	{
		ChangeFishDirectionIfNeed (_targetFood.transform.localPosition);

		float width = GetComponent<RectTransform> ().rect.width;
		float fixedPosX = 0;
		if (_targetFood.transform.localPosition.x > transform.localPosition.x) {
			fixedPosX = _targetFood.transform.localPosition.x - width * Mathf.Cos (transform.rotation.z) / 2;
		}
		if (_targetFood.transform.localPosition.x < transform.localPosition.x) {
			fixedPosX = _targetFood.transform.localPosition.x + width * Mathf.Cos (transform.rotation.z) / 2;
		}

		float fixedPosY = 0;
		if (_targetFood.transform.localPosition.y > transform.localPosition.y) {
			fixedPosY = _targetFood.transform.localPosition.y - width * Mathf.Sin (transform.rotation.z) / 2;
		}
		if (_targetFood.transform.localPosition.y < transform.localPosition.y) {
			fixedPosY = _targetFood.transform.localPosition.y + width * Mathf.Sin (transform.rotation.z) / 2;
		}

		Vector3 fixedPosition = new Vector3 (fixedPosX, fixedPosY);
		if (Vector3.SqrMagnitude (transform.localPosition - fixedPosition) > 0.5f) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, fixedPosition, Time.deltaTime * _fastSpeed);
		} else {
			EatFood ();
		}
	}

	void EatFood ()
	{
		_targetFood.OnEated ();
	}
}