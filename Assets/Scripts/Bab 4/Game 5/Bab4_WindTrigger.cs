using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_WindTrigger : MonoBehaviour {

	[Header("Status")]
	public bool up;
	public bool down;

	[Header("Reference To Another Object")]
	public GameObject btnOn;
	public GameObject btnOff;

	[Header("Reference To Another Script")]
	public Bab4_WindManager wM;

	void OnTriggerEnter2D(Collider2D other){
	
		btnOn.SetActive (true);
		btnOff.SetActive (false);
		if(up){
			wM.up = true;
		}
		if(down){
			wM.down = true;
		}
	
	}

	void OnTriggerExit2D(Collider2D other){
	
		btnOff.SetActive (true);
		btnOn.SetActive (false);
		if(up){
			wM.up = false;
		}
		if(down){
			wM.down = false;
		}
	
	}

}
