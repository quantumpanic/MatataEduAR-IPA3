using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_BadSmell : MonoBehaviour {

	[Header("Status")]
	public int hitPoint;
	public float bulletSpeed;
	public int damage;

	[Header("Reference To Another Object")]
	private GameObject player;
	private Transform target;

	[Header("Reference To Another Class")]
	public Global_Explosion pop;

	// Use this for initialization
	void Start () {

//		bossBody = GameObject.FindGameObjectWithTag ("Nimbus");
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		player = GameObject.FindGameObjectWithTag ("Player");

//		if(projectile){
//			bulletLifetime = 5f;
//		}
//		if(!projectile){
//			bulletLifetime = 2f;
//		}
//
//		bulletLifeTemp = bulletLifetime;

	}

	// Update is called once per frame
	void Update () {

//		if(projectile){
		float distanceToTarget = Vector3.Distance (transform.position, target.position);
		Vector3 targetDir = target.position - transform.position;
		float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 2);
		transform.Translate (Vector3.left * Time.deltaTime * bulletSpeed);
//		}

		if (hitPoint <= 0) {
			player.GetComponent<Bab3_Kamper> ().AddScore (1f);
//			Destroy (gameObject);
			PopTheProj();
		}

//		bulletLifetime -= Time.deltaTime;
//		if(bulletLifetime <= 0f && projectile){
//			PopTheProj ();
//		}
//		if(bulletLifetime <= 0f && !projectile){
//			DontPopTheProj ();
//		}
//		if(!bossBody.activeInHierarchy){
//			PopTheProj ();
//		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
//			bool temp = GameObject.FindGameObjectWithTag ("Player").GetComponent<Bab3_Kamper>().invincible;
//			if(!temp){
//				GameObject.FindGameObjectWithTag ("DmgOutput").GetComponent<Bab3_DamageOutput> ().ShowNumber (damage);
//			}
			other.gameObject.GetComponent<Bab3_Kamper> ().TakeDamage (damage);
			PopTheProj ();
		}

	}

	void PopTheProj(){

		Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
//		gameObject.SetActive(false);
		Destroy (gameObject);

	}

	public void TakeDamage(int dmg){

		hitPoint -= dmg;

	}

}
