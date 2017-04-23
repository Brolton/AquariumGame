using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : SFMonoBehaviour<object> {

	public enum Events {
		ON_DESTROY
	}

	float deltaMove = 35;

	public Fish hunterFish = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y - deltaMove * Time.deltaTime, transform.localPosition.z);

		if (transform.localPosition.y < -(transform.parent.GetComponent<RectTransform> ().rect.height / 2 - GetComponent<RectTransform> ().rect.height / 2)) {
			CallEvent ((int)Events.ON_DESTROY, this);
			Destroy (this.gameObject);
		}
	}

	public void OnEated() {
		CallEvent ((int)Events.ON_DESTROY, this);
		Destroy (this.gameObject);
	}
}
