using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	public InputField inputPoleLength;
	public InputField inputPoleWeight;
	public InputField inputCartWeight;
	public InputField inputTheta;

	public InvertedPendulum pendulum;

	float poleLength;
	float poleWeight;
	float cartWeight;
	float startTheta;

	// Use this for initialization
	void Start () {
		poleLength = 2.0f;
		poleWeight = 0.5f;
		cartWeight = 5.0f;
		startTheta = 180.0f;

		pendulum.SetPoleLength (poleLength);
		pendulum.SetPoleWeight (poleWeight);
		pendulum.SetCartWeight (cartWeight);
		pendulum.SetStartTheta (startTheta);

		inputPoleLength.text = string.Format ("{0:0.00}", poleLength);
		inputPoleWeight.text = string.Format ("{0:0.00}", poleWeight);
		inputCartWeight.text = string.Format ("{0:0.00}", cartWeight);
		inputTheta.text = string.Format ("{0:0.00}", startTheta);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdatePoleLength() {
		poleLength = float.Parse (inputPoleLength.text);
		if (poleLength < 0.1f || poleLength > 4.0f)
			poleLength = 1.0f;
		pendulum.SetPoleLength (poleLength);
		inputPoleLength.text = string.Format ("{0:0.00}", poleLength);
	}

	public void UpdatePoleWeight() {
		poleWeight = float.Parse (inputPoleWeight.text);
		if (poleWeight < 0.5f || poleWeight > 10.0f)
			poleWeight = 1.0f;
		pendulum.SetPoleWeight (poleWeight);
		inputPoleWeight.text = string.Format ("{0:0.00}", poleWeight);
	}

	public void UpdateCartWeight() {
		cartWeight = float.Parse (inputCartWeight.text);
		if (cartWeight < 0.5f || cartWeight > 10.0f)
			cartWeight = 2.0f;
		pendulum.SetCartWeight (cartWeight);
		inputCartWeight.text = string.Format ("{0:0.00}", cartWeight);
	}

	public void UpdateStartTheta() {
		startTheta = float.Parse (inputTheta.text);
		pendulum.SetStartTheta (startTheta);
		inputTheta.text = string.Format ("{0:0.00}", startTheta);
	}
}
