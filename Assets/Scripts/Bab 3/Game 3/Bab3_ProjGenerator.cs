using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_ProjGenerator : MonoBehaviour {

	[Header("Status")]
	public int randomNumber;
	public float timeBetweenShot;

	[Header("Additional Status")]
	//control which bullet comes out after Roll over, ex : move 1 means buff card, move 2 means onion, etc...
	public bool startRolling;
	public bool move1;
	public bool move2;
	public bool move3;
	public bool move4;
	public bool move5;
	public bool move6;

	[Header("Reference To Another Object")]
	public GameObject player;

	[Header("Reference To Another Class")]
	public Global_GameManager gM;
	public Bab3_Buff bullet1;
	public Bab3_BadSmell bullet2;
	public Bab3_BadSmell bullet3;
	public Bab3_BadSmell bullet4;
	public Bab3_BadSmell bullet5;
	public Bab3_BadSmell bullet6;

	[Header("Others")]
	private float timeStamp;

	// Use this for initialization
	void Start () {

		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if(player.GetComponent<Bab3_Kamper> ().curScore / player.GetComponent<Bab3_Kamper> ().score < .33f){
			randomNumber = Random.Range (1, 62);
			timeBetweenShot = 5f;
		}
		if(player.GetComponent<Bab3_Kamper> ().curScore / player.GetComponent<Bab3_Kamper> ().score >= .33f){
			randomNumber = Random.Range (1, 81);
			timeBetweenShot = 4f;
		}
		if(player.GetComponent<Bab3_Kamper> ().curScore / player.GetComponent<Bab3_Kamper> ().score >= .66f){
			randomNumber = Random.Range (1, 101);
			timeBetweenShot = 3f;
		}

		if (randomNumber <= 7) {
			move1 = true;
			move2 = false;
			move3 = false;
			move4 = false;
			move5 = false;
			move6 = false;
		}
		else if (randomNumber <= 24) {
			move1 = false;
			move2 = true;
			move3 = false;
			move4 = false;
			move5 = false;
			move6 = false;
		}
		else if (randomNumber <= 43) {
			move1 = false;
			move2 = false;
			move3 = true;
			move4 = false;
			move5 = false;
			move6 = false;
		}
		else if (randomNumber <= 62) {
			move1 = false;
			move2 = false;
			move3 = false;
			move4 = true;
			move5 = false;
			move6 = false;
		}
		else if (randomNumber <= 81) {
			move1 = false;
			move2 = false;
			move3 = false;
			move4 = false;
			move5 = true;
			move6 = false;
		}
		else if (randomNumber <= 100) {
			move1 = false;
			move2 = false;
			move3 = false;
			move4 = false;
			move5 = false;
			move6 = true;
		}

		if(gM.gameStart){

			if (move1) {
				if(Time.time >= timeStamp){
					timeStamp = Time.time + timeBetweenShot;
					Bab3_Buff newBullet = Instantiate (bullet1, transform.position, transform.rotation) as Bab3_Buff;

					newBullet.transform.position = transform.position;
					newBullet.transform.rotation = transform.rotation;
				}
			}
			if (move2) {
				if (Time.time >= timeStamp) {
					timeStamp = Time.time + timeBetweenShot;
					Bab3_BadSmell newBullet2 = Instantiate (bullet2, transform.position, transform.rotation) as Bab3_BadSmell;

					newBullet2.transform.position = transform.position;
					newBullet2.transform.rotation = transform.rotation;
				}
			}
			if (move3) {
				if (Time.time >= timeStamp) {
					timeStamp = Time.time + timeBetweenShot;
					Bab3_BadSmell newBullet3 = Instantiate (bullet3, transform.position, transform.rotation) as Bab3_BadSmell;

					newBullet3.transform.position = transform.position;
					newBullet3.transform.rotation = transform.rotation;
				}
			}
			if (move4) {
				if (Time.time >= timeStamp) {
					timeStamp = Time.time + timeBetweenShot;
					Bab3_BadSmell newBullet4 = Instantiate (bullet4, transform.position, transform.rotation) as Bab3_BadSmell;

					newBullet4.transform.position = transform.position;
					newBullet4.transform.rotation = transform.rotation;
				}
			}
			if (move5) {
				if (Time.time >= timeStamp) {
					timeStamp = Time.time + timeBetweenShot;
					Bab3_BadSmell newBullet5 = Instantiate (bullet5, transform.position, transform.rotation) as Bab3_BadSmell;

					newBullet5.transform.position = transform.position;
					newBullet5.transform.rotation = transform.rotation;
				}
			}
			if (move6) {
				if (Time.time >= timeStamp) {
					timeStamp = Time.time + timeBetweenShot;
					Bab3_BadSmell newBullet6 = Instantiate (bullet6, transform.position, transform.rotation) as Bab3_BadSmell;

					newBullet6.transform.position = transform.position;
					newBullet6.transform.rotation = transform.rotation;
				}
			}

		}

	}

}
