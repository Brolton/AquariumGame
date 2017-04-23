using System;
using UnityEngine;
using UnityEngine.UI;

public class FishViewController : SFMonoBehaviour<object>
{
	public enum Events {
		FISH_INVISIBLE
	}

	Color _color;
	public Color Color
	{
		get {
			return _color;
		}
		set {
			_color = value;
			_body.color = _color;
			_mouth.color = _color;
			_tail.color = _color;
		}
	}

	[SerializeField]
	Image _body;
	[SerializeField]
	Image _mouth;
	[SerializeField]
	Image _tail;

	[SerializeField]
	Image _eyeNormal;
	[SerializeField]
	Image _eyeCross;

	bool _fadeOut = false;

	Vector2 _baseSize;

	void Start() {
		_baseSize = GetComponent<RectTransform> ().rect.size;

		_eyeNormal.gameObject.SetActive (true);
		_eyeCross.gameObject.SetActive (false);
	}

	public void ChangeEyeToCross()
	{
		_eyeNormal.gameObject.SetActive (false);
		_eyeCross.gameObject.SetActive (true);
	}

	public void PlayDeathAnimation()
	{
		ChangeEyeToCross ();
		Color = Constants.DeathColor;
		_fadeOut = true;
	}

	void Update() {
		if (_fadeOut) {
			float newAlpha = Color.a - 0.005f; // Just for test
			if (newAlpha < 0) {
				newAlpha = 0;
			}
			Color = new Color (Constants.DeathColor.r, Constants.DeathColor.g, Constants.DeathColor.b, newAlpha);
			_eyeCross.color = new Color (1, 1, 1, newAlpha);
			if (newAlpha == 0) {
				_fadeOut = false;
				CallEvent((int)Events.FISH_INVISIBLE, null);
			}
		}
	}

	public void Grow(Fish.FishSizes newFishSize) {
		if (newFishSize == Fish.FishSizes.CHILD) {
			GetComponent<RectTransform> ().sizeDelta = new Vector2 (_baseSize.x * 2, _baseSize.y * 2);
		}
		if (newFishSize == Fish.FishSizes.ADULT) {
			GetComponent<RectTransform> ().sizeDelta = new Vector2 (_baseSize.x * 3, _baseSize.y * 3);
		}
	}
}

