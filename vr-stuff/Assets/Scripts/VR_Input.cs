using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Valve.VR;

public class VR_Input : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId grip = EVRButtonId.k_EButton_Grip;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;
	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

	SteamVR_Controller.Device controller;
	SteamVR_TrackedObject trackedObj;

	Holdable holdable;
	Holdable currentlyHolding;

	[Tooltip("Toggles these objects activity")]
	public GameObject[] models;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	void Update () {

		// Hold an object
		if (controller.GetPressDown(grip) && holdable != null) {
			currentlyHolding = holdable;
			currentlyHolding.Hold(transform);
			ToggleModelsActive();
		}		

		// Holds and snaps an object
		if (controller.GetPressDown(touchPad) && holdable != null) {
			currentlyHolding = holdable;
			currentlyHolding.HoldSnap(transform);
			ToggleModelsActive();
		}

		// Drops the held object
		if ((controller.GetPressUp(grip) || controller.GetPressUp(touchPad)) && currentlyHolding != null) {
			currentlyHolding.Drop(controller.velocity, controller.angularVelocity);
			currentlyHolding = null;
			ToggleModelsActive();
		}

		// If held object is gun, shoot
		if (controller.GetPressDown(trigger) && currentlyHolding != null) {
			var gun = currentlyHolding.gameObject.GetComponent<Shooting>();
			if (gun != null) {
				gun.Shoot();
			}
		}
		
		// Scene reset
		if (controller.GetPressDown(menu)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void ToggleModelsActive () {
		for (int i = 0; i < models.Length; i++) {
			models[i].SetActive(!models[i].activeSelf);
		}
	}

	void OnTriggerEnter (Collider c) {
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
