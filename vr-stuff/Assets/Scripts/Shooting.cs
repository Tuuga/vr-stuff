using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject bullet;
	
	public float force;

	Transform barrelEnd;

	void Start () {
		barrelEnd = transform.Find("Pivot").Find("Barrel").Find("BarrelEnd");
	}

	// Debug
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Shoot();
		}
	}

	public void Shoot () {
		GameObject bulletIns = (GameObject)Instantiate(bullet, barrelEnd.position, Quaternion.identity);
		Rigidbody irb = bulletIns.GetComponent<Rigidbody>();
		irb.velocity = transform.forward * force;
	}
}
