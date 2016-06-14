using UnityEngine;
using System.Collections;

public class Debug_Input : MonoBehaviour {

	Rigidbody rb;

	Holdable holdable;
	Holdable currentlyHolding;

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

		// If held object is gun, shoot
		if (Input.GetMouseButtonDown(0) && currentlyHolding != null) {
			var gun = currentlyHolding.GetComponent<Shooting>();
			if (gun != null) {
				gun.Shoot();
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.GetComponent<Holdable>() != null) {
			holdable = c.GetComponent<Holdable>();
			holdable.SetHighlightColor();
		}
	}

	void OnTriggerExit (Collider c) {
		var holdableExit = c.GetComponent<Holdable>();
		if (holdableExit != null)
			holdableExit.ResetColor();

		holdable = null;
	}
}
