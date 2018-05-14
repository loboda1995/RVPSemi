using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTheta : MonoBehaviour {

	public Material mat;
	public GameObject frameObject;
	public GameObject dataObject;

	public GameObject pendulum;
	private int posX = 0;
	private LineRenderer line;
	private LineRenderer frame;

	// Use this for initialization
	void Start () {
		frame = frameObject.AddComponent<LineRenderer>();
		frame.positionCount = 4;
		frame.widthMultiplier = 0.004f;
		frame.SetPosition (0, new Vector3 (-160f, -65, -1));
		frame.SetPosition (1, new Vector3 (-160f, 75, -1));
		frame.SetPosition (2, new Vector3 (180f, 75, -1));
		frame.SetPosition (3, new Vector3 (180f, -65, -1));
		frame.useWorldSpace = false;
		frame.loop = true;
		frame.material = mat;

		line = dataObject.AddComponent<LineRenderer>();
		line.widthMultiplier = 0.002f;
		line.useWorldSpace = false;
		line.material = mat;
		InvokeRepeating("GetTheta", 0.0f, 0.025f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void GetTheta () {
		if (posX == 399) {
			Vector3 pos;
			for (int i = 1; i < posX; i++) {
				pos = line.GetPosition (i);
				pos.x = i * 0.85f - 158;
				line.SetPosition (i - 1, pos);
			}
		}

		float value = pendulum.transform.localEulerAngles.y;
		if (value > 180)
			value = 360 - value;
		if (posX < 399) {
			posX++;
			line.positionCount = posX;
		}
		line.SetPosition (posX-1, new Vector3 (posX * 0.85f - 158, value / 1.304f - 64, -1));

	}
}
