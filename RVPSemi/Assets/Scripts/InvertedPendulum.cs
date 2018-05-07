using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedPendulum : MonoBehaviour {


	public GameObject pendulum;
	public GameObject cart;

	float poleLength = 1.0f;
	public float poleTheta;
	float poleThetaDot = 0f;
	float poleThetaDotDot = 0f;
	float pendulumMass = 1.0f;

	float cartX;
	float cartXDot = 0f;
	float cartXDotDot = 0f;
	float cartMass = 1.0f;
	float cartForce = 0f;
	float cartMu = 2f;

	// Use this for initialization
	void Start () {
		poleTheta = (pendulum.transform.localEulerAngles.y - 180) * Mathf.Deg2Rad;
		cartX = cart.transform.position.x;
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
}
