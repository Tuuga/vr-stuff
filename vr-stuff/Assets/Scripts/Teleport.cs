using UnityEngine;
using System.Collections;
using Valve.VR;

public class Teleport : MonoBehaviour {

	public static void Tele (Transform pointer, Transform teleportable) {
		RaycastHit hit;
		if (Physics.Raycast(pointer.position, pointer.forward, out hit, Mathf.Infinity)) {
			teleportable.position = new Vector3(hit.point.x, pointer.position.y, hit.point.z);
			SteamVR_Fade.Start(Color.black, 0);
			SteamVR_Fade.Start(Color.clear, 1);
		}
	}
}
