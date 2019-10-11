using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Weather_Manager : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject light_rain;
	public GameObject heavy_rain;

	[Header("Reference To Another Script")]
	public Bab6_BossManager boss;

	void Start() {

		light_rain.SetActive (false);
		heavy_rain.SetActive (false);

	}

	// Update is called once per frame
	void Update () {

		if(boss.stratus){
			light_rain.SetActive (true);
			heavy_rain.SetActive (false);
		}
		if(boss.nimbus){
			light_rain.SetActive (false);
			heavy_rain.SetActive (true);
		}

	}

}
