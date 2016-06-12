using UnityEngine;
using System.Collections;

public class InputTesting : MonoBehaviour {

	Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;


	SteamVR_Controller.Device controller;
	SteamVR_TrackedObject trackedObj;

	Transform triggerPoint;
	Transform currentlyHolding;
	Transform holdable;

	Vector3 onHoldPos;
	Quaternion onHoldRot;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		triggerPoint = transform.Find("TriggerPoint");
	}

	void Update () {

		if (controller.GetPressDown(trigger)) {
			print(name + " Trigger");
			if (holdable != null) {
				currentlyHolding = holdable;
				currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
				onHoldPos = currentlyHolding.position;
				onHoldRot = currentlyHolding.rotation;
			}
		}
		if (controller.GetPressUp(trigger)) {
			currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
			currentlyHolding.GetComponent<Rigidbody>().velocity = controller.velocity;
			currentlyHolding.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
			currentlyHolding = null;
		}

		if (currentlyHolding != null) {
			currentlyHolding.position = triggerPoint.position;
			currentlyHolding.rotation = transform.rotation;
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
