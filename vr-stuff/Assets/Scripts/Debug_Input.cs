using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Debug_Input : MonoBehaviour {

	Rigidbody rb;

	Holdable holdable;
	Holdable currentlyHolding;
	bool holdToggle;

	Button button;

	SliderScript slider;
	List<SliderScript> currentSliders = new List<SliderScript>();


	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void Update () {

		// Hold an object
		if (Input.GetKeyDown(KeyCode.E) && holdable != null) {
			currentlyHolding = holdable;
			currentlyHolding.Hold(transform);
		}

		// Holds and snaps an object
		if (Input.GetKeyDown(KeyCode.R) && holdable != null) {
			currentlyHolding = holdable;
			currentlyHolding.HoldSnap(transform);
		}

		// Drops the held object
		if ((Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.R)) && currentlyHolding != null) {
			currentlyHolding.Drop(rb.velocity, rb.angularVelocity);
			currentlyHolding = null;
		}

		// Hold snap toggle
		if (Input.GetKeyDown(KeyCode.T)) {
			if (!holdToggle && holdable != null) {
				currentlyHolding = holdable;
				currentlyHolding.HoldSnap(transform);
				holdToggle = true;
			} else if (holdToggle && currentlyHolding != null) {
				currentlyHolding.Drop(rb.velocity, rb.angularVelocity);
				currentlyHolding = null;
				holdToggle = false;
			}
		}

		// Press button
		if (Input.GetKeyDown(KeyCode.F) && button != null) {
			button.Press();
		}

		if (Input.GetKey(KeyCode.E) && slider != null) {
			slider.Move(transform);
		}

		// If held object is gun, shoot
		if (Input.GetMouseButtonDown(0) && currentlyHolding != null) {
			var gun = currentlyHolding.GetComponent<Shooting>();
			if (gun != null) {
				gun.Shoot();
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			Teleport.Tele(transform.parent, transform.parent);
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.GetComponent<Holdable>() != null) {
			holdable = c.GetComponent<Holdable>();
			holdable.SetHighlightColor();

		} else if (c.GetComponent<Button>() != null) {
			button = c.GetComponent<Button>();
			
		} else if (c.transform.parent.parent.GetComponent<SliderScript>() != null) {
			var newSlider = c.transform.parent.parent.GetComponent<SliderScript>();
			currentSliders.Add(newSlider);
			slider = currentSliders[0];
		}
	}

	void OnTriggerExit(Collider c) {
		var holdableExit = c.GetComponent<Holdable>();
		if (holdableExit != null)
			holdableExit.ResetColor();

		if (c.GetComponent<Holdable>() != null)
			holdable = null;

		if (c.GetComponent<Button>() != null)
			button = null;

		var sliderExit = c.transform.parent.parent.GetComponent<SliderScript>();

		if (currentSliders.Contains(sliderExit))
			currentSliders.Remove(sliderExit);

		if (currentSliders.Count > 0) {
			slider = currentSliders[0];
		} else {
			slider = null;
		}
	}
}
