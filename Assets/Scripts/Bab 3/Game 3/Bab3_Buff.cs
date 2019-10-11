using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_Buff : MonoBehaviour {

	[Header("Status")]
	public int hitPoint;
	public float bulletSpeed;

	[Header("Reference To Another Object")]
	private GameObject player;
	private Transform target;

	[Header("Reference To Another Class")]
	public Global_Explosion pop;


	// Use this for initialization
	void Start () {

		target = GameObject.FindGameObjectWithTag ("Player").transform;
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

		float distanceToTarget = Vector3.Distance (transform.position, target.position);
		Vector3 targetDir = target.position - transform.position;
		float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 2);
		transform.Translate (Vector3.left * Time.deltaTime * bulletSpeed);

		if (hitPoint <= 0) {
			Destroy (gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Bab3_Kamper> ().buffed = true;
			other.gameObject.GetComponent<Bab3_Kamper> ().AddBuffTime (7f);
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
