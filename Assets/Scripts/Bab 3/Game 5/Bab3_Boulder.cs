using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_Boulder : MonoBehaviour {

	[Header("Properties")]
	private Rigidbody2D rb;

	void Start(){
		
		rb = GetComponent<Rigidbody2D> ();
		rb.mass = 100000;

	}

	void OnCollisionStay2D(Collision2D other){

		if(other.gameObject.tag == "Solid")
		{
			rb.mass = 1;
//			rb.bodyType = RigidbodyType2D.Dynamic;	
		}
		if(other.gameObject.tag == "Liquid")
		{
			rb.mass = 100000;
//			rb.bodyType = RigidbodyType2D.Kinematic;
		}
		if(other.gameObject.tag == "Gas")
		{
			rb.mass = 100000;
//			rb.bodyType = RigidbodyType2D.Kinematic;
		}

	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Solid")
		{
			rb.mass = 1;
//			rb.bodyType = RigidbodyType2D.Dynamic;	
		}
		if(other.gameObject.tag == "Liquid")
		{
			rb.mass = 100000;
//			rb.bodyType = RigidbodyType2D.Kinematic;
		}
		if(other.gameObject.tag == "Gas")
		{
			rb.mass = 100000;
//			rb.bodyType = RigidbodyType2D.Kinematic;
		}

	}


}
