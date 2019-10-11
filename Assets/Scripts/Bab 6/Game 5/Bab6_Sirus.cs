using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Sirus : MonoBehaviour {

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
	public Transform at12;
	public Transform at13;
	public Transform at21;
	public Transform at22;
	public Transform at23;
	public Transform at24;
	public Transform at25;
	public Transform waypoint1;
	public Transform waypoint2;
	public GameObject sirusPlate;
	public GameObject explosion;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;
	public Bab6_BossManager bM;
	public Global_ObjPooler pool;

	[Header("Others")]
	public float tempAtt;

	// Use this for initialization
	void Start () {
		
		percentage = hitPoint * 30 / 100;
		sirusPlate.SetActive (false);

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
			transform.position = Vector3.MoveTowards (transform.position, waypoint2.position, 1f * Time.deltaTime);
		}
		else if (way2 && !way1){
			transform.position = Vector3.MoveTowards (transform.position, waypoint1.position, 1f * Time.deltaTime);
		}
		else{
			transform.position = Vector3.MoveTowards (transform.position, waypoint1.position, 1f * Time.deltaTime);
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
			sirusPlate.SetActive (true);
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
			bM.sirus = false;
			bM.stratus = true;
		}

	}

	void OnDisable(){
		Instantiate(explosion, transform.position, transform.rotation);
	}

	public void TakeDamage(int dmg) {
		
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

			GameObject newBullet2 = pool.GetBullet ();
			newBullet2.transform.position = at12.position;
			newBullet2.transform.rotation = at12.rotation;
			newBullet2.SetActive (true);

			GameObject newBullet3 = pool.GetBullet ();
			newBullet3.transform.position = at13.position;
			newBullet3.transform.rotation = at13.rotation;
			newBullet3.SetActive (true);

		}

		if(move2){
			GameObject newBullet4 = pool.GetBullet ();
			newBullet4.transform.position = at21.position;
			newBullet4.transform.rotation = at21.rotation;
			newBullet4.SetActive (true);

			GameObject newBullet5 = pool.GetBullet ();
			newBullet5.transform.position = at22.position;
			newBullet5.transform.rotation = at22.rotation;
			newBullet5.SetActive (true);

			GameObject newBullet6 = pool.GetBullet ();
			newBullet6.transform.position = at23.position;
			newBullet6.transform.rotation = at23.rotation;
			newBullet6.SetActive (true);

			GameObject newBullet7 = pool.GetBullet ();
			newBullet7.transform.position = at24.position;
			newBullet7.transform.rotation = at24.rotation;
			newBullet7.SetActive (true);

			GameObject newBullet8 = pool.GetBullet ();
			newBullet8.transform.position = at25.position;
			newBullet8.transform.rotation = at25.rotation;
			newBullet8.SetActive (true);
		}

	}

}
