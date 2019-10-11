using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_WindManager : MonoBehaviour {

	[Header("Status")]
	public float speed;

	[Header("Additional Status")]
	public bool up;
	public bool down;

	[Header("Reference To Another Object")]
	public GameObject windMill;
	public Transform topLimit;
	public Transform botLimit;

	// Update is called once per frame
	void Update () {

		if (up && !down) {
			windMill.transform.position = Vector3.MoveTowards (windMill.transform.position, topLimit.position, speed * Time.deltaTime);
		} else if (down && !up) {
			windMill.transform.position = Vector3.MoveTowards (windMill.transform.position, botLimit.position, speed * Time.deltaTime);
		} else {
			windMill.transform.position = windMill.transform.position;
		}

	}

}
