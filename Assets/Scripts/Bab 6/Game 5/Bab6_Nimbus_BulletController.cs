using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Nimbus_BulletController : MonoBehaviour {

	[Header("Status")]
	public float bulletSpeed;
	public float bulletLifetime;

	[Header("Additional Status")]
	[Tooltip("Check It If U Want This To Be Projectile")]
	public bool projectile;

	[Header("Reference To Another Object")]
	public GameObject bossBody;
	public Transform target;

	[Header("Reference To Another Script")]
	public Global_Explosion pop;

	[Header("Others")]
	public float bulletLifeTemp;

	// Use this for initialization
	void Start () {

		bossBody = GameObject.FindGameObjectWithTag ("Nimbus");
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		if(projectile){
			bulletLifetime = 5f;
		}
		if(!projectile){
			bulletLifetime = 2f;
		}

		bulletLifeTemp = bulletLifetime;
		bulletSpeed = 4f;

	}
	
	// Update is called once per frame
	void Update () {

		if(projectile){
			float distanceToTarget = Vector3.Distance (transform.position, target.position);
			Vector3 targetDir = target.position - transform.position;
			float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 2);
			transform.Translate (Vector3.left * Time.deltaTime * bulletSpeed);
		}
		bulletLifetime -= Time.deltaTime;
		if(bulletLifetime <= 0f && projectile){
			PopTheProj ();
		}
		if(bulletLifetime <= 0f && !projectile){
			DontPopTheProj ();
		}
		if(!bossBody.activeInHierarchy){
			PopTheProj ();
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Bab6_Player> ().TakeDamage (1);
			if (projectile) {
				PopTheProj ();
			}
		}

	}

	void PopTheProj(){
		
		Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
		bulletLifetime = bulletLifeTemp;
		gameObject.SetActive(false);

	}

	void DontPopTheProj(){

		bulletLifetime = bulletLifeTemp;
		gameObject.SetActive(false);

	}

}
