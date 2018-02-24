using System.Linq;
using UnityEngine;
public class CameraLightSwitcher : MonoBehaviour {

	private Light dLight {
		get {
			if (_dLight == null) {
				_dLight = FindObjectsOfType<Light>().First(x => x.type == LightType.Directional);
				dir = _dLight.transform.rotation;
				flippedDir = Quaternion.LookRotation(Vector3.Reflect(_dLight.transform.forward, Vector3.forward));
			}
			return _dLight;
		}
	}
	private Light _dLight;

	public bool flipped;
	private static Quaternion dir, flippedDir;

	void OnPreRender() {
		if (flipped) dLight.transform.rotation = flippedDir;
		else dLight.transform.rotation = dir;

	}
}