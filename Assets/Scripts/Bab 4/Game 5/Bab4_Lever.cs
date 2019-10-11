using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_Lever : MonoBehaviour {

	[Header("Properties")]
	public Animator ceiling;

	[Header("Reference To Another Object")]
	public GameObject leverDown;
	public GameObject leverUp;

	void Start(){
		
		ceiling.enabled = false;
		leverDown.SetActive (true);
		leverUp.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			ceiling.enabled = true;
			leverDown.SetActive (false);
			leverUp.SetActive (true);
		}

	}

}
