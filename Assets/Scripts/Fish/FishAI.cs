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

	int _fishSpeed = 50; // for tests

	Vector3 _targetPos;

	// Use this for initialization
	void Start () {
		rightBound = transform.parent.GetComponent<RectTransform> ().rect.width / 2 + this.GetComponent<RectTransform> ().rect.width / 4;
		topBound = transform.parent.GetComponent<RectTransform> ().rect.height / 2 - this.GetComponent<RectTransform> ().rect.height;

		GenetateNewTargetPosAndSpeed ();
	}

	// Update is called once per frame
	void Update () {
		if (_aiIsActive) {
			MoveAi ();
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
		transform.Rotate(0,0,180);
		_tail.StopMoving();
		_mouth.StopMoving();
	}

	void MoveAi ()
	{
		if ( Vector3.SqrMagnitude(transform.localPosition - _targetPos) > 0.0001 ) {
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
		ChangeFishDirectionIfNeed ();

		Vector3 heading = _targetPos - transform.localPosition;
		float distance = heading.magnitude;

		_fishSpeed = UnityEngine.Random.Range (_fishSpeed / 2, _fishSpeed + 1);
	}

	void ChangeFishDirectionIfNeed() {
		// check need direction
		if (_targetPos.x - transform.localPosition.x < 0 &&
			transform.rotation.eulerAngles.y != 0) {
			transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.x);
		}
		if (_targetPos.x - transform.localPosition.x > 0 &&
			transform.rotation.eulerAngles.y != 180) {
			transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.x);
		}
			
//		var vectorToTarget = _targetPos - transform.localPosition;
//		vectorToTarget.Normalize ();
//
//		float rot_z = Mathf.Atan2(vectorToTarget.y, Mathf.Abs(vectorToTarget.x)) * Mathf.Rad2Deg;
//		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rot_z - 90);

//		var heading = _targetPos - transform.localPosition;
//		var distance = heading.magnitude;
//		var direction = heading / distance; // This is now the normalized direction.
////		transform.localRotation.SetEulerAngles(direction);
//
//		var q = Quaternion.LookRotation(heading);
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100 * Time.deltaTime);
	}
}