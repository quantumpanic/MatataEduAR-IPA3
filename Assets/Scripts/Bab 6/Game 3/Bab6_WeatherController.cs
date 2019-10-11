using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_WeatherController : MonoBehaviour {

	public Bab6_HotnChillController temperature;
	public GameObject hot;
	public GameObject cold;
	public ParticleSystem snow;

	// Use this for initialization
	void Start () {

		var em = snow.emission;
		em.enabled = false;
		hot.SetActive (false);
		cold.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if (temperature.curTemperature >= 70f/100f * temperature.maxTemperature) {
			hot.SetActive (true);
			cold.SetActive (false);
		} else if (temperature.curTemperature <= 30f/100f * temperature.maxTemperature) {
			cold.SetActive (true);
			hot.SetActive (false);
		} else {
			hot.SetActive (false);
			cold.SetActive (false);
		}

	}

	void LateUpdate(){
	
		if(cold.activeInHierarchy){
			var em = snow.emission;
			em.enabled = true;
		}
		if(!cold.activeInHierarchy){
			var em = snow.emission;
			em.enabled = false;
		}
	
	}

}
