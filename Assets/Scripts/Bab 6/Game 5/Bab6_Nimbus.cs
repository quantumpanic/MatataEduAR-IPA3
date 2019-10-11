using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Nimbus : MonoBehaviour {

	[Header("Status")]
	public int hitPoint;
	public int randomNumber;
	public float attackTime;

	[Header("Additional Status")]
	public int percentage;
	public bool canShoot;
	public bool startRolling;
	public bool move1;
	public bool move2;
	public bool way1;
	public bool way2;

	[Header("Properties")]
	public Animator anim;

	[Header("Reference To Another Object")]
	public Transform at11;
	public Transform at21;
	public Transform waypoint1;
	public Transform waypoint2;
	public GameObject toBlack;

	[Header("Reference To Another Script")]
	public Global_ObjPooler pool;
	public Global_ObjPooler pool2;
	public Global_GameManager gM;
	public Bab6_BossManager bM;

	[Header("Others")]
	public float tempAtt;

	// Use this for initialization
	void Start () {

		percentage = hitPoint * 50 / 100;
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();

		way1 = false;
		way2 = false;

		tempAtt = attackTime;

		move1 = false;
		move2 = false;

//		bulletSpeed = 5f;
		attackTime = 4f;

	}
	
	// Update is called once per frame
	void Update () {

		if(hitPoint <= percentage){
			anim.speed = 2f;
		}

		if (way1 && !way2){
			transform.position = Vector3.MoveTowards (transform.position, waypoint2.position, .5f * Time.deltaTime);
		}
		else if (way2 && !way1){
			transform.position = Vector3.MoveTowards (transform.position, waypoint1.position, .5f * Time.deltaTime);
		}
		else{
			transform.position = Vector3.MoveTowards (transform.position, waypoint1.position, .5f * Time.deltaTime);
		}
		if (transform.position == waypoint1.position) {
			way1 = true;
			way2 = false;
		} 
		if (transform.position == waypoint2.position) {
			way2 = true;
			way1 = false;
		}

		if(gM.gameStart){
			if(canShoot){
				startRolling = true;
				StartCoroutine(waitBeforeAttack());
				tempAtt = attackTime;
			}

			if(!canShoot){
				tempAtt -= Time.deltaTime;
				if(tempAtt <= 0){
					move1 = false;
					move2 = false;
					canShoot = true;
				}
			}
		}


		if(move1 || move2){
			anim.SetBool ("isAttack", true);
		}

		if(!move1 && !move2){
			anim.SetBool ("isAttack", false);
		}

		if(hitPoint <= 0){
			GlobalGameManager.Instance.ScoreAdd(3000);
			bM.nimbus = false;
			gM.clear = true;
		}

	}

	public void TakeDamage(int dmg){
		
		hitPoint -= dmg;

	}

	IEnumerator waitBeforeAttack(){

		yield return new WaitForSeconds (4);
		if(startRolling){
			startRolling = false;
			randomNumber = Random.Range (1, 3);
		}
		if (randomNumber == 1) {
			move1 = true;
		} else {
			move2 = true;
		}
		canShoot = false;

	}

	public void Attack(){

		if(move1){
			GameObject newBullet = pool.GetBullet ();
			newBullet.transform.position = at11.position;
			newBullet.transform.rotation = at11.rotation;
			newBullet.SetActive (true);

//			Bab6_Nimbus_BulletController newBullet = Instantiate (bullet, at11.position, at11.rotation) as Bab6_Nimbus_BulletController;
		}
		if(move2){
			GameObject newBullet2 = pool2.GetBullet ();
			newBullet2.transform.position = at21.position;
			newBullet2.transform.rotation = at21.rotation;
			newBullet2.SetActive (true);

//			Bab6_Nimbus_BulletController newBullet2 = Instantiate (bullet, at21.position, at21.rotation) as Bab6_Nimbus_BulletController;
//			newBullet2.speed = bulletSpeed;
		}

	}

}