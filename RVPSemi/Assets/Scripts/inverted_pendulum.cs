using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class inverted_pendulum : MonoBehaviour {
	float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
