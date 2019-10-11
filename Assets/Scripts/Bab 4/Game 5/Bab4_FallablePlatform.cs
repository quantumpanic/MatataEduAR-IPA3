using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bab4_FallablePlatform : MonoBehaviour {

	[Header("Properties")]
	private EdgeCollider2D colider;

	// Use this for initialization
	void Start () {

		colider = gameObject.GetComponentInChildren<EdgeCollider2D> ();

	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				colider.isTrigger = true;
			}
		}

		#if UNITY_ANDROID || UNITY_IOS
		if(CrossPlatformInputManager.GetButtonDown("Down")){
			colider.isTrigger = true;
		}
		#endif

	}

	void OnCollisionStay2D(Collision2D other){

		if (other.gameObject.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				colider.isTrigger = true;
			}
		}

		#if UNITY_ANDROID || UNITY_IOS
		if(CrossPlatformInputManager.GetButtonDown("Down")){
			colider.isTrigger = true;
		}
		#endif

	}

	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "Player") {
			colider.isTrigger = false;
		}

	}

}
