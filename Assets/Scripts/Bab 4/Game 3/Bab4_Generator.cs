using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_Generator : MonoBehaviour {

	[Header("Status")]
	public int randomNumber;
	public float timeBetweenShot;

	[Header("Reference To Another Object")]
	public Vector3 pos;

	[Header("Reference To Another Class")]
	public Global_GameManager gM;
	public Bab4_FallObject matahari;
	public Bab4_FallObject bom;

	[Header("Others")]
	private float temp;

	void Start(){

		temp = timeBetweenShot;

	}

	void FixedUpdate(){
	
		pos = new Vector3 (Random.Range(-7, 7) + transform.position.x, 8f, 0f);
	
	}

	void Update(){
	
		if(gM.gameStart){
			timeBetweenShot -= Time.deltaTime;

			if(timeBetweenShot <= 0f){
				randomNumber = Random.Range (1, 4);
				if (randomNumber <= 2) {
					Bab4_FallObject newobj = Instantiate (matahari, pos, Quaternion.identity) as Bab4_FallObject;
//				Bab4_Wind newWind = Instantiate (angin, one.transform.position, one.transform.rotation) as Bab4_Wind;
				} else {
					Bab4_FallObject newobj2 = Instantiate (bom, pos, Quaternion.identity) as Bab4_FallObject;
//				Bab4_Wind newWind3 = Instantiate (angin, three.transform.position, three.transform.rotation) as Bab4_Wind;
				}
				timeBetweenShot = temp;
			}
		}
	
	}

}
