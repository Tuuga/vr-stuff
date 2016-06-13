using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject bullet;
	public float force;
	public float fireRate;

	float lastShot;

	Transform barrelEnd;

	void Start () {
		barrelEnd = transform.Find("Pivot").Find("Barrel").Find("BarrelEnd");
	}

	public void Shoot () {
		if (lastShot < Time.time + fireRate) {
			GameObject bulletIns = (GameObject)Instantiate(bullet, barrelEnd.position, Quaternion.identity);
			Rigidbody rb = bulletIns.GetComponent<Rigidbody>();
			rb.velocity = barrelEnd.forward * force;
			lastShot = Time.time;
		}
	}
}
