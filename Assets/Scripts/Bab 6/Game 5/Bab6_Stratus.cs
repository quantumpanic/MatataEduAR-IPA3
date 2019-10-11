using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Stratus : MonoBehaviour {

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
	public bool move3;
	public bool way1;
	public bool way2;

	[Header("Properties")]
	public Animator anim;
	public Animator anim2;
	public Animator anim3;

	[Header("Reference To Another Object")]
	public Transform at11;
	public Transform at12;
	public Transform at13;
	public Transform at14;
	public Transform at15;
	public Transform at21;
	public Transform at22;
	public Transform at23;
	public Transform at24;
	public Transform at25;
	public Transform at31;
	public Transform at32;
	public Transform at33;
	public Transform at34;
	public Transform at35;
	public Transform waypoint1;
	public Transform waypoint2;
	public GameObject explosion;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;
	public Bab6_BossManager bM;
	public Global_ObjPooler pool;

	[Header("Others")]
	public float tempAtt;

	// Use this for initialization
	void Start () {

		percentage = hitPoint * 40 / 100;
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();

		way1 = false;
		way2 = false;

		tempAtt = attackTime;

		move1 = false;
		move2 = false;

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
					move3 = false;
					canShoot = true;
				}
			}
		}

		if(move1){
			anim.SetBool ("isAttack", true);
		}
		if(move2){
			anim2.SetBool ("isAttack", true);
		}
		if(move3){
			anim3.SetBool ("isAttack", true);
		}

		if(!move1 && !move2 && !move3){
			anim.SetBool ("isAttack", false);
			anim2.SetBool ("isAttack", false);
			anim3.SetBool ("isAttack", false);
		}

		if(hitPoint <= 0){
			GlobalGameManager.Instance.ScoreAdd(3000);
			bM.stratus = false;
			bM.nimbus = true;
		}

	}

	void OnDisable(){
		Instantiate(explosion, transform.position, transform.rotation);
	}

	public void TakeDamage(int dmg){
		
		hitPoint -= dmg;

	}

	IEnumerator waitBeforeAttack(){

		yield return new WaitForSeconds (4);
		if(startRolling){
			startRolling = false;
			randomNumber = Random.Range (1, 4);
		}
		if (randomNumber == 1) {
			move1 = true;
		} else if (randomNumber == 2) {
			move2 = true;
		} else {
			move3 = true;
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

			GameObject newBullet4 = pool.GetBullet ();
			newBullet4.transform.position = at14.position;
			newBullet4.transform.rotation = at14.rotation;
			newBullet4.SetActive (true);

			GameObject newBullet5 = pool.GetBullet ();
			newBullet5.transform.position = at15.position;
			newBullet5.transform.rotation = at15.rotation;
			newBullet5.SetActive (true);

//			Bab6_Stratus_BulletController newBullet = Instantiate (bullet, at11.position, at11.rotation) as Bab6_Stratus_BulletController;
//			newBullet.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet2 = Instantiate (bullet, at12.position, at12.rotation) as Bab6_Stratus_BulletController;
//			newBullet2.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet3 = Instantiate (bullet, at13.position, at13.rotation) as Bab6_Stratus_BulletController;
//			newBullet3.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet4 = Instantiate (bullet, at14.position, at14.rotation) as Bab6_Stratus_BulletController;
//			newBullet4.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet5 = Instantiate (bullet, at15.position, at15.rotation) as Bab6_Stratus_BulletController;
//			newBullet5.speedTemp = bulletSpeed;
		}

		if(move2){
			GameObject newBullet6 = pool.GetBullet ();
			newBullet6.transform.position = at21.position;
			newBullet6.transform.rotation = at21.rotation;
			newBullet6.SetActive (true);

			GameObject newBullet7 = pool.GetBullet ();
			newBullet7.transform.position = at22.position;
			newBullet7.transform.rotation = at22.rotation;
			newBullet7.SetActive (true);

			GameObject newBullet8 = pool.GetBullet ();
			newBullet8.transform.position = at23.position;
			newBullet8.transform.rotation = at23.rotation;
			newBullet8.SetActive (true);

			GameObject newBullet9 = pool.GetBullet ();
			newBullet9.transform.position = at24.position;
			newBullet9.transform.rotation = at24.rotation;
			newBullet9.SetActive (true);

			GameObject newBullet10 = pool.GetBullet ();
			newBullet10.transform.position = at25.position;
			newBullet10.transform.rotation = at25.rotation;
			newBullet10.SetActive (true);

//			Bab6_Stratus_BulletController newBullet6 = Instantiate (bullet, at21.position, at21.rotation) as Bab6_Stratus_BulletController;
//			newBullet6.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet7 = Instantiate (bullet, at22.position, at22.rotation) as Bab6_Stratus_BulletController;
//			newBullet7.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet8 = Instantiate (bullet, at23.position, at23.rotation) as Bab6_Stratus_BulletController;
//			newBullet8.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet9 = Instantiate (bullet, at24.position, at24.rotation) as Bab6_Stratus_BulletController;
//			newBullet9.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet10 = Instantiate (bullet, at25.position, at25.rotation) as Bab6_Stratus_BulletController;
//			newBullet10.speedTemp = bulletSpeed;
		}

		if(move3){
			GameObject newBullet11 = pool.GetBullet ();
			newBullet11.transform.position = at31.position;
			newBullet11.transform.rotation = at31.rotation;
			newBullet11.SetActive (true);

			GameObject newBullet12 = pool.GetBullet ();
			newBullet12.transform.position = at32.position;
			newBullet12.transform.rotation = at32.rotation;
			newBullet12.SetActive (true);

			GameObject newBullet13 = pool.GetBullet ();
			newBullet13.transform.position = at33.position;
			newBullet13.transform.rotation = at33.rotation;
			newBullet13.SetActive (true);

			GameObject newBullet14 = pool.GetBullet ();
			newBullet14.transform.position = at34.position;
			newBullet14.transform.rotation = at34.rotation;
			newBullet14.SetActive (true);

			GameObject newBullet15 = pool.GetBullet ();
			newBullet15.transform.position = at35.position;
			newBullet15.transform.rotation = at35.rotation;
			newBullet15.SetActive (true);

//			Bab6_Stratus_BulletController newBullet11 = Instantiate (bullet, at31.position, at31.rotation) as Bab6_Stratus_BulletController;
//			newBullet11.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet12 = Instantiate (bullet, at32.position, at32.rotation) as Bab6_Stratus_BulletController;
//			newBullet12.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet13 = Instantiate (bullet, at33.position, at33.rotation) as Bab6_Stratus_BulletController;
//			newBullet13.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet14 = Instantiate (bullet, at34.position, at34.rotation) as Bab6_Stratus_BulletController;
//			newBullet14.speedTemp = bulletSpeed;
//
//			Bab6_Stratus_BulletController newBullet15 = Instantiate (bullet, at35.position, at35.rotation) as Bab6_Stratus_BulletController;
//			newBullet15.speedTemp = bulletSpeed;
		}

	}

}
