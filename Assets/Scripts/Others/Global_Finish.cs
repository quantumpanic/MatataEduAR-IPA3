using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Finish : MonoBehaviour {

	[Header("Reference To Another Script")]
	public Global_GameManager gM;

	void Start(){
		
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();

	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Solid" || other.gameObject.tag == "Liquid" || other.gameObject.tag == "Gas" || other.gameObject.tag == "Player")
		{
			if(!gM.clear){
				gM.clear = !gM.clear;
			}
		}

	}

}
