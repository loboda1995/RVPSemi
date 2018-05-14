using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphs : MonoBehaviour {

	public GameObject graphTheta;
	public GameObject graphCart;
	public GameObject graphForce;

	public GameObject buttonShowGraphs;
	public GameObject buttonHideGraphs;

	public GameObject buttonShowTheta;
	public GameObject buttonShowCart;
	public GameObject buttonShowForce;

	// Use this for initialization
	void Start () {
		HideGraphs ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowGraphs() {
		ShowGraphTheta ();

		buttonShowGraphs.SetActive (false);
		buttonHideGraphs.SetActive (true);

		buttonShowTheta.SetActive (true);
		buttonShowCart.SetActive (true);
		buttonShowForce.SetActive (true);
	}

	public void HideGraphs() {
		buttonShowGraphs.SetActive (true);
		buttonHideGraphs.SetActive (false);

		buttonShowTheta.SetActive (false);
		buttonShowCart.SetActive (false);
		buttonShowForce.SetActive (false);

		graphTheta.SetActive (false);
		graphCart.SetActive (false);
		graphForce.SetActive (false);
	}

	public void ShowGraphTheta() {
		graphTheta.SetActive (true);
		graphCart.SetActive (false);
		graphForce.SetActive (false);
	}

	public void ShowGraphCart() {
		graphTheta.SetActive (false);
		graphCart.SetActive (true);
		graphForce.SetActive (false);
	}

	public void ShowGraphForce() {
		graphTheta.SetActive (false);
		graphCart.SetActive (false);
		graphForce.SetActive (true);
	}
}
