using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_WaterWay : MonoBehaviour {

	[Header("Properties")]
	public BoxCollider2D kolider;

	void OnTriggerStay2D(Collider2D other){
		
		if (other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas") {
			kolider.enabled = false;
		}

	}

	void OnTriggerExit2D(Collider2D other){
		
		if(other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas") {
			kolider.enabled = true;
		}	

	}
			
}
