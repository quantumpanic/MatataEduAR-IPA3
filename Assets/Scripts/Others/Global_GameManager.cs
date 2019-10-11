using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_GameManager : MonoBehaviour {

	[Header("Status")]
	public bool canProgress;
	public bool gameStart;
	public bool clear;
	public bool failed;

	[Header("Reference To Another Object")]
	public GameObject option;
	public GameObject objective;

	// Use this for initialization
	void Awake () {

		canProgress = true;
		gameStart = false;
		clear = false;
		failed = false;

	}

	// Update is called once per frame
	void Update () {

		if(canProgress){
			if(clear){
				gameStart = false;
				GameObject.Find("Objectives").SendMessage("Victory");
				canProgress = false;
			}
			if(failed){
				gameStart = false;
				GameObject.Find("Objectives").SendMessage("GameOver");
				canProgress = false;
			}
		}


	}

	public void Pause(){

		if(!option.activeInHierarchy){
			Instantiate (option);
			Time.timeScale = 0;
		}

	}

}
