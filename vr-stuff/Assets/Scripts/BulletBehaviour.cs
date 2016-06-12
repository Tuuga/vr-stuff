using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	void Start () {
		Destroy(gameObject, 5);
	}

	void OnCollisionEnter (Collision c) {
		Destroy(gameObject, 1);
	}
}
