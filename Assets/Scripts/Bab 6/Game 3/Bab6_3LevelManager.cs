using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_3LevelManager : MonoBehaviour {

	public Global_Timer tmr;
	public Global_GameManager gM;
	
	// Update is called once per frame
	void Update () {
		
		if(!gM.clear){
			if(tmr.curTime >= 120f){
				//3 stars
				print(tmr.curTime);
				gM.clear = true;
			}
		}

	}

}
