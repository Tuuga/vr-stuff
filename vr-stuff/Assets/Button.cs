using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour {

	public enum ButtonType { Reset, MakeMeBigger, MakeMeSmaller };
	public ButtonType bt;

	public void Press () {
		if (bt == ButtonType.Reset) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else if (bt == ButtonType.MakeMeBigger) {
			GameObject.Find("[CameraRig]").transform.localScale += Vector3.one;
		} else if (bt == ButtonType.MakeMeSmaller) {
			GameObject.Find("[CameraRig]").transform.localScale -= Vector3.one;
		}
	}
}
