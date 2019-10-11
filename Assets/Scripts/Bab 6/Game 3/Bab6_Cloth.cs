using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Cloth : MonoBehaviour {

	public GameObject winterCo;
	public GameObject summerCo;
	public GameObject burn;
	public GameObject frozen;
	public bool wntCoOn;
	public bool smrCoOn;

	public Bab6_Player player;
	public Bab6_HotnChillController temperature;

	// Use this for initialization
	void Start () {

		winterCo.SetActive (false);
		summerCo.SetActive (false);
		burn.SetActive (false);
		frozen.SetActive (false);
		wntCoOn = false;
		smrCoOn = false;

	}

	void Update(){
	
		if(wntCoOn){
			winterCo.SetActive (true);
			summerCo.SetActive (false);
		}
		if(smrCoOn){
			summerCo.SetActive (true);
			winterCo.SetActive (false);
		}

		if(wntCoOn && temperature.curTemperature >= 80f/100f * temperature.maxTemperature || !wntCoOn && !smrCoOn && temperature.curTemperature >= 80f/100f * temperature.maxTemperature){
			burn.SetActive (true);
			frozen.SetActive (false);
			player.TakeDamage (1);
		}
		else if(smrCoOn && temperature.curTemperature <= 20f/100f * temperature.maxTemperature || !wntCoOn && !smrCoOn && temperature.curTemperature <= 20f/100f * temperature.maxTemperature){
			frozen.SetActive (true);
			burn.SetActive (false);
			player.TakeDamage (1);
		}
		else{
			burn.SetActive (false);
			frozen.SetActive (false);
		}
	
	}

	public void WinterCo(){
	
		wntCoOn = true;
		smrCoOn = false;
	
	}

	public void SummerCo(){
	
		smrCoOn = true;
		wntCoOn = false;
	
	}

}
