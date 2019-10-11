using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global_Timer : MonoBehaviour {

	[Header("Status")]
	public float milliSeconds, seconds, minutes, curTime;
	public bool desc;

	[Header("Reference To Another Object")]
	public Text counterText;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;

	void Start(){
	
		if(desc){
			GlobalGameManager.Instance.ScoreAdd (3000);
		}
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();

	}

	// Update is called once per frame
	void Update () {

		if(gM.gameStart){
			if(desc){
				curTime += Time.deltaTime;
				GlobalGameManager.Instance.ScoreAdd(-10 * Time.deltaTime);
				if(GlobalGameManager.Instance.lastScore <= 0){
					GlobalGameManager.Instance.lastScore = 0;
				}
			}
			else{
				curTime += Time.deltaTime;
				GlobalGameManager.Instance.ScoreAdd(20 * Time.deltaTime);
			}
		}
		minutes = (int)(curTime / 60f);
		seconds = (int)(curTime % 60f);
		milliSeconds = (int)(curTime * 100);
		milliSeconds = milliSeconds % 100;
		counterText.text = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
//		counterText.text = minutes + " : " + seconds + " : " + milliSeconds;

	}
}
