using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_PortalManager : MonoBehaviour {

	[Header("Properties")]
	public CapsuleCollider2D targetC, teleport;

	void OnTriggerEnter2D(Collider2D other){

		targetC.enabled = false;

	}

	void OnTriggerExit2D(Collider2D other){

		teleport.enabled = true;

	}
}
