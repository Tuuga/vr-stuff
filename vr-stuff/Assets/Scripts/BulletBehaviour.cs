using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	[Tooltip("Time to destroy on start")]
	public float initialDestroyTime;
	[Tooltip("Time to destroy on collision")]
	public float destroyTimeOnHit;

	void Start () {
		Destroy(gameObject, initialDestroyTime);
	}

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag != "Trigger") {
			Destroy(gameObject, destroyTimeOnHit);
		}
	}
}
