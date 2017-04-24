using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltageArrow : MonoBehaviour {
	public GameObject mainController;
	public float zAngle;
	public float currentPower;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainController != null) {
			currentPower = AquaCondition.currentPower;
			zAngle = -currentPower/3.25f;
			gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler (new Vector3(0,0,zAngle));
		}
	}
}
