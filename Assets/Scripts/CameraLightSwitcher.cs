using UnityEngine;

public class CameraLightSwitcher : MonoBehaviour {

	public GameObject enableSun, disableSun;

	void OnPreRender() {
		enableSun.SetActive(true);
		disableSun.SetActive(false);
	}
}