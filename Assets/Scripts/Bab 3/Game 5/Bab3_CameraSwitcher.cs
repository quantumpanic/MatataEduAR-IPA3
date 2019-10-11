using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_CameraSwitcher : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject currentCamera;
	public GameObject cameraTarget;

	void OnTriggerStay2D(Collider2D other){

		if(other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas"){
			currentCamera.SetActive(false);
			cameraTarget.SetActive(true);
		}

	}

}
