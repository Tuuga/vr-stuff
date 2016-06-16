using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderScript : MonoBehaviour {

	public Transform handle;
	public Text valueText;

	float value;

	public void Move (Transform t) {

		handle.position = t.position;
		float z = Mathf.Clamp(handle.localPosition.z, -0.5f, 0.5f);
		handle.localPosition = new Vector3(0, 1, z);

		value = 0.5f + z; // 0 - 1 range
		valueText.text = "" + Mathf.Round(value * 100) / 100;
	}

	public float GetValue () {
		return value;
	}
}
