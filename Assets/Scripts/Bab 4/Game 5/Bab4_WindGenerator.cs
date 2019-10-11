using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_WindGenerator : MonoBehaviour {

	[Header("Status")]
	public int randomNumber;
	public float timeBetweenShot;

	[Header("Reference To Another Object")]
	public Transform one;
	public Transform two;
	public Transform three;

	[Header("Reference To Another Class")]
	public Bab4_Wind angin;

	[Header("Others")]
	private float temp;

	void Start(){

		temp = timeBetweenShot;

	}

	// Update is called once per frame
	void Update () {

		timeBetweenShot -= Time.deltaTime;

		if(timeBetweenShot <= 0f){
			randomNumber = Random.Range (1, 4);
			if (randomNumber == 1) {
				Bab4_Wind newWind = Instantiate (angin, one.transform.position, one.transform.rotation) as Bab4_Wind;
			} else if (randomNumber == 2) {
				Bab4_Wind newWind2 = Instantiate (angin, two.transform.position, two.transform.rotation) as Bab4_Wind;
			} else {
				Bab4_Wind newWind3 = Instantiate (angin, three.transform.position, three.transform.rotation) as Bab4_Wind;
			}
			timeBetweenShot = temp;
		}
			
	}

}
