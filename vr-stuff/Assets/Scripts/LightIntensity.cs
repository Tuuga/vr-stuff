using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour {

	public GameObject slider;
	SliderScript ss;

	void Start () {
		ss = slider.GetComponent<SliderScript>();
	}

	void Update () {
		GetComponent<Light>().intensity = ss.GetValue();
	}
}
