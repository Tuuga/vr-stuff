using UnityEngine;
using System.Collections;

public class Holdable : MonoBehaviour {

	Rigidbody rb;
	Transform parent;
	Color normalColor;

	public Color highlight;
	public GameObject[] models;

	void Start () {
		rb = GetComponent<Rigidbody>();
		parent = transform.parent;
		normalColor = models[0].GetComponent<Renderer>().material.color;
	}

	/// <summary>
	/// Sets the object as a child
	/// </summary>
	public void Hold (Transform t) {
		if (t.GetComponentInChildren<Holdable>() == null) {
			ToggleKinematic();
			transform.parent = t;
		}
	}

	/// <summary>
	/// Sets the object as child and resets position and rotation
	/// </summary>
	public void HoldSnap (Transform t) {
		if (t.GetComponentInChildren<Holdable>() == null) {
			ToggleKinematic();
			transform.parent = t;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
	}

	/// <summary>
	/// Removes the child object and sets velocity and angular velocity
	/// </summary>
	public void Drop (Vector3 vel, Vector3 angVel) {
		ToggleKinematic();
		transform.parent = parent;
		rb.velocity = vel;
		rb.angularVelocity = angVel;
	}

	public void SetHighlightColor () {
		for (int i = 0; i < models.Length; i++) {
			models[i].GetComponent<Renderer>().material.color = highlight;
		}
	}

	public void ResetColor () {
		for (int i = 0; i < models.Length; i++) {
			models[i].GetComponent<Renderer>().material.color = normalColor;
		}
	}
	
	void ToggleKinematic () {
		if (rb != null) {
			rb.isKinematic = (!rb.isKinematic);
		}
	}
}
