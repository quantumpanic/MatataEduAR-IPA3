using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_Teleport : MonoBehaviour {

	[Header("Reference To Another Object")]
	public Transform targetT;

	[Header("Reference To Another Script")]
	public Bab3_Player player;

	void Start(){

		GameObject temp = GameObject.Find("Player");
		player = temp.GetComponent<Bab3_Player>();

	}

	void OnTriggerEnter2D(Collider2D other){

		player.transform.position = targetT.transform.position;

	}

}
