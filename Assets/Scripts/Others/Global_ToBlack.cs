using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_ToBlack : MonoBehaviour {

	public Global_GameManager gM;

	void Start(){
	
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();
	
	}

	public void ToBlack(){
	
		gM.failed = true;
	
	}

}
