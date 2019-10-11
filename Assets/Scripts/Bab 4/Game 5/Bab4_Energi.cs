using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_Energi : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject energi_hidup;
	public GameObject energi_mati;
	public Animator kipas;
	public GameObject benda;

	// Use this for initialization
	void Start () {
		
		benda.SetActive (true);
		energi_hidup.SetActive (true);
		energi_mati.SetActive (false);

	}

	void OnCollisionEnter2D(Collision2D other){
		
		if(other.gameObject.tag == "Player"){
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			energi_hidup.SetActive (false);
			energi_mati.SetActive (true);
			benda.SetActive (false);
			kipas.enabled = false;
		}

	}

}
