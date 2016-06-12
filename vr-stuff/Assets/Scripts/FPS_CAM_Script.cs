using UnityEngine;
using System.Collections;

public class FPS_CAM_Script : MonoBehaviour {

	public float movSpeed;
	public float mouseSens;
	public float upDownRange;

	float verticalRotation;
	float horizontalRotation;

	bool mouseLock;

	public KeyCode upKey;
	public KeyCode downKey;

	GameObject mainCam;

	Quaternion baseRotation = Quaternion.identity;

	void Start () {

		MouseLock();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update () {

		if (mouseLock)
			MouseLook();

		if (Input.GetKeyDown(KeyCode.LeftAlt))
			MouseLock();

		CamMovement();		
	}

	void MouseLook () {
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

		horizontalRotation += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

		transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0) * baseRotation;

		mainCam.transform.position = transform.position;
		mainCam.transform.rotation = transform.rotation;
	}

	void MouseLock () {
		mouseLock = !mouseLock;
		if (mouseLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void CamMovement() {

		if (Input.GetKey(KeyCode.W))
			transform.position += transform.forward * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.S))
			transform.position += -transform.forward * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
			transform.position += -transform.right * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.D))
			transform.position += transform.right * movSpeed * Time.deltaTime;

		if (Input.GetKey(upKey))
			transform.position += transform.up * movSpeed * Time.deltaTime;

		if (Input.GetKey(downKey))
			transform.position += -transform.up * movSpeed * Time.deltaTime;
	}
}
