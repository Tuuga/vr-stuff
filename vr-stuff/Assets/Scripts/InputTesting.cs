using UnityEngine;
using System.Collections;

public class InputTesting : MonoBehaviour {

	Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	Valve.VR.EVRButtonId grip = Valve.VR.EVRButtonId.k_EButton_Grip;


	SteamVR_Controller.Device controller;
	SteamVR_TrackedObject trackedObj;

	Transform triggerPoint;
	Transform currentlyHolding;
	Transform holdable;

	Vector3 onHoldPos;
	Quaternion onHoldRot;

	bool holding;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		triggerPoint = transform.Find("TriggerPoint");
	}

	void Update () {

		if (controller.GetPressDown(grip) && !holding) {
			print(name + " Trigger");
			if (holdable != null) {
				holding = true;
				currentlyHolding = holdable;
				currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
				onHoldPos = currentlyHolding.position;
				onHoldRot = currentlyHolding.rotation;
			}
		} else if (controller.GetPressDown(grip) && holding) {
			holding = false;
			currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
			currentlyHolding.GetComponent<Rigidbody>().velocity = controller.velocity;
			currentlyHolding.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
			currentlyHolding = null;
		}

		if (currentlyHolding != null) {
			currentlyHolding.position = triggerPoint.position;
			currentlyHolding.rotation = transform.rotation;
			if (controller.GetPressDown(trigger)) {
				Shooting gun = currentlyHolding.GetComponent<Shooting>();
				if (gun != null) {
					gun.Shoot();
				}
			}
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
