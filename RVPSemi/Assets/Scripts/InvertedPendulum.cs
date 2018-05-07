using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedPendulum : MonoBehaviour {


	public GameObject pendulum;
	public GameObject cart;

	float poleLength;
	public float poleTheta;
	float poleThetaDot = 0f;
	float poleThetaDotDot = 0f;
	float pendulumMass;

	float cartX;
	float cartXDot = 0f;
	float cartXDotDot = 0f;
	float cartMass;
	float cartForce = 0f;
	float cartMu = 5f;

	float startX = 0f;
	float startTheta = 20f;
	float startPoleLength = 1.0f;
	float startPoleWeight = 1.0f;
	float startCartWeight = 2.0f;

	// Use this for initialization
	void Start () {
		poleTheta = (startTheta - 180) * Mathf.Deg2Rad;
		cartX = startX;
		poleLength = startPoleLength;
		pendulumMass = startPoleWeight;
		cartMass = startCartWeight;

		pendulum.transform.localRotation = Quaternion.Euler(pendulum.transform.localRotation.x, startTheta, pendulum.transform.localRotation.z);
		cart.transform.position = new Vector3(cartX, cart.transform.position.y, cart.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		float delta = Time.deltaTime;
		/*cartForce = 0f;
		if (Input.GetKey (KeyCode.LeftArrow))
			cartForce = -10f;
		if (Input.GetKey (KeyCode.RightArrow))
			cartForce = 10f;*/

		float cosTheta = Mathf.Cos(poleTheta);
		float sinTheta = Mathf.Sin(poleTheta);
		float totalMass = cartMass + pendulumMass;
		float massLength = pendulumMass * poleLength;

		poleThetaDotDot = (Physics.gravity.y * totalMass * sinTheta + cosTheta * (cartForce - massLength * Mathf.Pow(poleThetaDot, 2) * sinTheta - cartMu * cartXDot));
		cartXDotDot = (cartForce + massLength * (poleThetaDotDot * cosTheta - Mathf.Pow(poleThetaDot, 2) * sinTheta) - cartMu * cartXDot) / totalMass;

		cartXDot = cartXDot + delta * cartXDotDot;
		cartX = cartX + delta * cartXDot;
		poleThetaDot = poleThetaDot + delta * poleThetaDotDot;
		poleTheta = poleTheta + delta * poleThetaDot;

		pendulum.transform.Rotate (0, delta * poleThetaDot * Mathf.Rad2Deg, 0);
		cart.transform.Translate (delta * cartXDot, 0, 0);
	}

	public void SetForce(float f) {
		cartForce = f;
	}

	public void SetPoleLength(float l) {
		startPoleLength = l;
	}

	public void SetPoleWeight(float w) {
		startPoleWeight = w;
	}

	public void SetCartWeight(float w) {
		startCartWeight = w;
	}

	public void SetStartTheta(float t) {
		startTheta = t;
	}

	public void Reset() {
		cartX = startX;
		poleTheta = (startTheta - 180) * Mathf.Deg2Rad;
		poleLength = startPoleLength;
		pendulumMass = startPoleWeight;
		cartMass = startCartWeight;
		pendulum.transform.localRotation = Quaternion.Euler(pendulum.transform.localRotation.x, startTheta, pendulum.transform.localRotation.z);
		cart.transform.position = new Vector3(cartX, cart.transform.position.y, cart.transform.position.z);
		cartXDot = 0f;
		cartXDotDot = 0f;
		poleThetaDot = 0f;
		poleThetaDotDot = 0f;
		cartForce = 0f;
	}
}
