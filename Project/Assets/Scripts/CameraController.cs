using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	float zoomAmount = 0f; //With Positive and negative values
	float maxToClamp = 10f;
	float ROTSpeed = 10f;

	void Update() {
		zoomAmount += Input.GetAxis("Mouse ScrollWheel");
		zoomAmount = Mathf.Clamp(zoomAmount, -maxToClamp, maxToClamp);
		float translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), maxToClamp - Mathf.Abs(zoomAmount));
		gameObject.transform.Translate(0,0,translate * ROTSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
	}
}
