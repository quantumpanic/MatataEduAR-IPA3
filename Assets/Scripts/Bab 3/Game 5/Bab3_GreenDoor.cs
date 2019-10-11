using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_GreenDoor : MonoBehaviour {

	[Header("Properties")]
	public Animator anim;

	void Start(){

		anim = GetComponentInChildren<Animator> ();

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas"){
			anim.SetBool ("isOpen", true);
		}
	
	}

	void OnTriggerExit2D(Collider2D other){

		if(other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas"){
			anim.SetBool ("isOpen", false);
		}

	}

}
