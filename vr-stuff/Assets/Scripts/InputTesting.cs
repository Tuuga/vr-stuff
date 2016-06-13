using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputTesting : MonoBehaviour {

	Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	Valve.VR.EVRButtonId grip = Valve.VR.EVRButtonId.k_EButton_Grip;
	Valve.VR.EVRButtonId menu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

	SteamVR_Controller.Device controller;
	SteamVR_TrackedObject trackedObj;

	Transform triggerPoint;
	Transform currentlyHolding;
	Transform holdable;

	bool holding;

	GameObject model;
	GameObject triggerModel;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		triggerPoint = transform.Find("TriggerPoint");
		model = transform.Find("Model").gameObject;
		triggerModel = triggerPoint.Find("Model").gameObject;
	}

	void Update () {

		// Grabs an object
		if (controller.GetPressDown(grip) && !holding) {
			print(name + " Trigger");
			if (holdable != null) {
				holding = true;
				currentlyHolding = holdable;
				currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
				model.SetActive(false);
				triggerModel.SetActive(false);
			}
			// Drops the object that you are holding
		} else if (controller.GetPressDown(grip) && holding) {
			holding = false;
			currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
			currentlyHolding.GetComponent<Rigidbody>().velocity = controller.velocity;
			currentlyHolding.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
			currentlyHolding = null;
			model.SetActive(true);
			triggerModel.SetActive(true);
		}

		if (currentlyHolding != null) {
			currentlyHolding.position = transform.position;
			currentlyHolding.rotation = transform.rotation;

			// If holding a gun and trigger pulled -> shoot
			if (controller.GetPressDown(trigger)) {
				Shooting gun = currentlyHolding.GetComponent<Shooting>();
				if (gun != null) {
					gun.Shoot();
				}
			}
		}
		
		// Scene reset
		if (controller.GetPressDown(menu)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void OnTriggerEnter (Collider c) {
		if (c.tag == "Movable") {
			print(name + " - " + c.name);
			holdable = c.transform;
		}
	}
	void OnTriggerExit (Collider c) {
		if (c.transform == holdable) {
			holdable = null;
		}
	}
}
