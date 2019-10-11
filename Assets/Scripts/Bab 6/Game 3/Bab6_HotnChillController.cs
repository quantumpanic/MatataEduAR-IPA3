using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bab6_HotnChillController : MonoBehaviour {

	public bool tempUp;
	public bool tempDown;
	public Global_GameManager gM;
	public int randomNumber;
	public float timeBetweenRoll;
	public float maxTemperature;
	public float curTemperature;
	public Slider slide;

	[Header("Others")]
	private float temp;

	// Use this for initialization
	void Start () {

		tempUp = false;
		tempDown = false;
		temp = timeBetweenRoll;
		curTemperature = maxTemperature / 2f;

	}
	
	// Update is called once per frame
	void Update () {

		slide.value = curTemperature / maxTemperature;

		if(gM.gameStart){
			timeBetweenRoll -= Time.deltaTime;
			if(timeBetweenRoll <= 0f){
				randomNumber = Random.Range (1, 3);
				if (randomNumber == 1) {
					tempUp = true;
					tempDown = false;
				} else {
					tempDown = true;
					tempUp = false;

				}
				timeBetweenRoll = temp;
			}
		}

		if(tempUp){
			if(curTemperature >= maxTemperature){
				curTemperature = maxTemperature;
			}

			curTemperature += Time.deltaTime;	
		}
		if(tempDown){
			if(curTemperature <= 0f){
				curTemperature = 0f;
			}
			curTemperature -= Time.deltaTime;
		}

	}

}
