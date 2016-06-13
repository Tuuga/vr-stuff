using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	void Start () {
		Destroy(gameObject, 5);
	}

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag != "Trigger") {
			Destroy(gameObject, 1);
		}
	}
}
