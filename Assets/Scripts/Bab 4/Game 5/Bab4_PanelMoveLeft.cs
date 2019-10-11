using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_PanelMoveLeft : MonoBehaviour {

	public Bab4_MovingPlatform mP;
//	public GameObject panel;
//	public GameObject otherObj;
//	private Vector3 offsetToObj;

	void Update(){

//		if (otherObj != null) {
//			otherObj.transform.position = transform.position + offsetToObj;
//			panel.transform.Translate (Vector2.left * 3f * Time.deltaTime);
//		}

	}

	void OnTriggerEnter2D(Collider2D other){

//		if (other.gameObject.tag == "Player") {
			
//		}

	}

	void OnTriggerStay2D(Collider2D other){

		mP.way1 = true;
//		otherObj = other.gameObject;
//		offsetToObj = otherObj.transform.position - transform.position;

	}

	void OnTriggerExit2D(Collider2D other){

		mP.way1 = false;
//		if (other.gameObject.tag == "Player") {
//		panel.transform.position = panel.transform.position;
//		otherObj = null;
//		}

	}

}
