using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailEnd : MonoBehaviour {

	public InvertedPendulum pendulum;

	void OnTriggerEnter(Collider col) {
		pendulum.Reset ();
	}
}
