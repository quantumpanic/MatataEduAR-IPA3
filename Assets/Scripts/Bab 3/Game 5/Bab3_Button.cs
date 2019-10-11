using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_Button : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject doorRed;
	public GameObject doorGreen;
	public GameObject buttonUnppress;
	public GameObject buttonPressed;

	void Start(){

		doorRed.SetActive (true);
		doorGreen.SetActive (false);
		buttonUnppress.SetActive (true);
		buttonPressed.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Solid"){
			doorRed.SetActive (false);
			doorGreen.SetActive (true);
			buttonUnppress.SetActive (false);
			buttonPressed.SetActive (true);
		}

	}

}
