using UnityEngine;
using System.Collections;

public class Debug_Input : MonoBehaviour {

	Rigidbody rb;

	Holdable holdable;
	Holdable currentlyHolding;
	bool holdToggle;

	Button button;

	SliderScript slider;

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
		} else if (c.transform.root.GetComponent<SliderScript>() != null) {
			slider = c.transform.root.GetComponent<SliderScript>();
		} 
	}

	void OnTriggerExit (Collider c) {
		var holdableExit = c.GetComponent<Holdable>();
		if (holdableExit != null)
			holdableExit.ResetColor();

		holdable = null;
		button = null;
		slider = null;
	}
}
