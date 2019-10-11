using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Player_BulletController : MonoBehaviour {

	[Header("Status")]
	public float bulletSpeed;

	[Header("Reference To Another Object")]
	public GameObject playerBody;

	[Header("Reference To Another Script")]
	public Global_Explosion pop;

//	bool CekUjung (Vector3 pos) {
//		
//		Vector3 posTemp = Camera.allCameras [0].WorldToScreenPoint(pos);
//		transform.position = posTemp;
//		if (posTemp.x > Screen.width || posTemp.x < Screen.width || posTemp.y > Screen.height || posTemp.y < Screen.height) {
//			PopTheProj ();
//			return true;
//		} else {
//			return false;
//		}
//
//	}

	// Use this for initialization
	void Start () {

		playerBody = GameObject.FindGameObjectWithTag ("Player");
		bulletSpeed = 10f;

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
//		if(screenPosition.x < widthThresold.x || screenPosition.x > widthThresold.y || screenPosition.y < heightThresold.x || screenPosition.y > heightThresold.y){
//			PopTheProj ();
//		}
//		if(transform.position.x > Screen.width / 2 || transform.position.x < Screen.width - Screen.width){
//			PopTheProj ();
//		}
//		if(transform.position.y > Screen.height / 2 || transform.position.y < Screen.height - Screen.height){
//			PopTheProj ();
//		}

		transform.Translate (Vector2.right * bulletSpeed * Time.deltaTime);
//		bulletLifetime -= Time.deltaTime;
//		if(bulletLifetime <= 0f){
//			PopTheProj ();
//		}
		if(!playerBody.activeInHierarchy){
			PopTheProj ();
		}

//		if(!gameObject.renderer){
//			PopTheProj ();
//		}

	}

//	public void OnBecameInvisible(){
//		print ("upil");
//		PopTheProj ();
//	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Sirus") {
			other.gameObject.GetComponentInParent<Bab6_Sirus> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
			PopTheProj ();
		}else if(other.gameObject.tag == "Stratus"){
			other.gameObject.GetComponentInParent<Bab6_Stratus> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
			PopTheProj ();
		}else if(other.gameObject.tag == "Kumulus"){
			other.gameObject.GetComponentInParent<Bab6_Nimbus> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
			PopTheProj ();
		}else if(other.gameObject.tag == "badSmell"){
			other.gameObject.GetComponentInParent<Bab3_BadSmell> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
			PopTheProj ();
		}else if(other.gameObject.tag == "buffCard"){
			other.gameObject.GetComponentInParent<Bab3_Buff> ().TakeDamage (1);
			Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
		}

	}

	void PopTheProj(){
		
		Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
//		Destroy (gameObject);
//		bulletLifetime = bulletLifeTemp;
		gameObject.SetActive(false);

	}

	void DontPopTheProj(){

		gameObject.SetActive(false);

	}

}