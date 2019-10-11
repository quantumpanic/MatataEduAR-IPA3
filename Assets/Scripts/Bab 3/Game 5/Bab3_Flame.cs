using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bab3_Flame : MonoBehaviour {

	[Header("Reference To Another Script")]
	public Bab3_Player player;

	void Start(){

		GameObject temp = GameObject.Find("Player");
		player = temp.GetComponent<Bab3_Player>();

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid"){
			player.burning = true;
		}

	}

	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid") {
			player.burning = false;
		}

	}

}