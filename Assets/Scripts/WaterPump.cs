using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WaterPump : MonoBehaviour {

	public GameObject waterParent;
	public UnityEvent onActivate;
	public UnityEvent onDeactivate;

	private bool on;
	private bool busy;

	public void Interact() {
		if (busy) return;
		busy = true;
		on = !on;
		if (on) StartCoroutine(RaiseWater());
		else StartCoroutine(LowerWater());
	}

	IEnumerator RaiseWater() {
		Vector3 pos = waterParent.transform.position;

		for (float t = 0; t < 1; t += 2 * Time.deltaTime) {
			pos.y = Mathf.Lerp(-0.9f, 0, t);
			waterParent.transform.position = pos;
			yield return null;
		}
		pos.y = 0;
		waterParent.transform.position = pos;
		busy = false;
		onActivate.Invoke();
	}

	IEnumerator LowerWater() {
		Vector3 pos = waterParent.transform.position;

		for (float t = 0; t < 1; t += 2 * Time.deltaTime) {
			pos.y = Mathf.Lerp(0, -0.9f, t);
			waterParent.transform.position = pos;
			yield return null;
		}
		pos.y = -0.9f;
		waterParent.transform.position = pos;
		busy = false;
		onDeactivate.Invoke();
	}
}