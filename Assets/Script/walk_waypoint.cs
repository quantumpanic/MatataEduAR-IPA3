using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk_waypoint : MonoBehaviour {

	public Transform player ;
	public Transform head;
	Animator Chicken_Walk;

	string state = "patrol";
	public GameObject[] waypoints;
	int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;
	float accuracyWP = 5.0f;

	// Use this for initialization
	void Start () {

		Chicken_Walk = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;

		if (state == "patrol" && waypoints.Length > 0) 
		{
			Chicken_Walk.SetBool ("isIdle", false);
			Chicken_Walk.SetBool ("isWalking", true);
			if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuracyWP) {


				currentWP = Random.Range (0, waypoints.Length);
				//currentWP++;
				//if (currentWP >= waypoints.Length) {
				//	currentWP = 0;
				//}
			}

			//rotate towards waypoint
			direction = waypoints [currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * speed);

		}


	}
}