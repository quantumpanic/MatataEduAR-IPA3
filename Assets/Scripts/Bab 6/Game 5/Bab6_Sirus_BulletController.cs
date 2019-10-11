using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Sirus_BulletController : MonoBehaviour {

	[Header("Status")]
	public float bulletSpeed;

	[Header("Reference To Another Object")]
	public GameObject bossBody;

	[Header("Reference To Another Script")]
	public Global_Explosion pop;

	// Use this for initialization
	void Start () {

		bossBody = GameObject.FindGameObjectWithTag ("Sirus");
//		bulletLifetime = 3f;
//		bulletLifeTemp = bulletLifetime;
		bulletSpeed = 5f;

	}

	// Update is called once per frame
	void Update () {

		Vector2 bulletPosition = Camera.allCameras [0].WorldToScreenPoint(transform.position);
		if(bulletPosition.x > Screen.width || bulletPosition.y > Screen.height ){
			DontPopTheProj ();
		}
		if(bulletPosition.x < 0 || bulletPosition.y < 0){
			DontPopTheProj ();
		}

		transform.Translate (Vector2.left * bulletSpeed * Time.deltaTime);
//		bulletLifetime -= Time.deltaTime;
//		if(bulletLifetime <= 0f){
//			PopTheProj ();
//		}
		if(!bossBody.activeInHierarchy){
			PopTheProj ();
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Bab6_Player> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
			PopTheProj ();
		}

	}

	void PopTheProj(){
		
		Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
//		bulletLifetime = bulletLifeTemp;
		gameObject.SetActive(false);

	}

	void DontPopTheProj(){

		gameObject.SetActive(false);

	}

}
