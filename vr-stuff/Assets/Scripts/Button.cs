using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour {

	public enum ButtonType { None, Reset, MakeMeBigger, MakeMeSmaller };
	public ButtonType bt;

	Transform rig;

	void Start () {
		rig = GameObject.Find("[CameraRig]").transform;
	}

	public void Press () {
		if (bt == ButtonType.Reset) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else if (bt == ButtonType.MakeMeBigger) {
			rig.localScale += Vector3.one / 4;
		} else if (bt == ButtonType.MakeMeSmaller) {
			rig.localScale -= Vector3.one / 4;
		}
	}
}
