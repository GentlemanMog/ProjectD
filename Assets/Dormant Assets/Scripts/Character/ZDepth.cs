using UnityEngine;
using System.Collections;

public class ZDepth : MonoBehaviour {

	private float Ypos;
	private float Xpos;

	// Update is called once per frame
	void Update () {
		Xpos = transform.position.x;
		Ypos = transform.position.y;
		transform.position = new Vector3 (Xpos, Ypos, Ypos);
	}
}
