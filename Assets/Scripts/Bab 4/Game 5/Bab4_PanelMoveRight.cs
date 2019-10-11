using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_PanelMoveRight : MonoBehaviour {

	public Bab4_MovingPlatform mP;
//	public GameObject panel;
//	public GameObject otherObj;
//	public Vector3 offsetToObj;
//	public Transform target;

	void Update(){

//		if (otherObj != null) {
//			offsetToObj = otherObj.transform.position - panel.transform.position;
//			otherObj.transform.position = panel.transform.position + offsetToObj;
//		}
	
	}

	void OnTriggerEnter2D(Collider2D other){



	}

	void OnTriggerStay2D(Collider2D other){

		mP.way2 = true;
//		panel.transform.position = Vector3.MoveTowards (panel.transform.position, target.position, 5f * Time.deltaTime);
//		otherObj = other.gameObject;
//		panel.transform.Translate (Vector2.right * 3f * Time.deltaTime);


	}

	void OnTriggerExit2D(Collider2D other){

		mP.way2 = false;
//		panel.transform.position = panel.transform.position;
//		otherObj = null;

	}

}
