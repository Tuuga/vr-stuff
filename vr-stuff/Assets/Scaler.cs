using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

	public SliderScript ss;
	public Transform cameraRig;

	void Update () {
		cameraRig.localScale = Vector3.one * Mathf.Clamp(ss.GetValue(), 0.1f, 1f);
	}
}
