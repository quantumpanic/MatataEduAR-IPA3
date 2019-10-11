using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_MovingPlatform : MonoBehaviour {

	[Header("Status")]
	public float speed;

	[Header("Additional Status")]
	[Tooltip("This Object Is Moving To WaypointOne")]
	public bool way1;
	[Tooltip("This Object Is Moving To WaypointTwo")]
	public bool way2;
	[Tooltip("This Object Is Moving By Trigger")]
	public bool moveWithTrigger;
	private Vector3 offsetToObj;

	[Header("Reference To Another Object")]
	[Tooltip("Object To Chase, Leave Null If Not Used")]
	public Transform target;
	[Tooltip("Waypoint To Patrol, Leave Null If Not Used")]
	public Transform waypointOne;
	[Tooltip("Waypoint To Patrol, Leave Null If Not Used")]
	public Transform waypointTwo;
	private GameObject otherObj;

	void Start(){

		if(waypointOne != null && waypointTwo != null && !moveWithTrigger){
			way1 = true;
			way2 = false;
		}
	
	}

	// Update is called once per frame
	void Update () {

		//patrol script
		if(way1 && target == null){
			transform.position = Vector3.MoveTowards (transform.position, waypointOne.position, speed * Time.deltaTime);
		}

		if(way2 && target == null){
			transform.position = Vector3.MoveTowards (transform.position, waypointTwo.position, speed * Time.deltaTime);
		}

		//trigger the patrol script if two waypoint isnt null
		if(!moveWithTrigger){
			if(waypointOne != null && waypointTwo != null) {
				if(transform.position == waypointOne.position) {
					way1 = !way1;
					way2 = !way2;
				}

				if(transform.position == waypointTwo.position) {
					way1 = !way1;
					way2 = !way2;
				}
			} 
		}

		if(target != null){
			float temp;
			Vector3 targetPos = transform.position;
			temp = target.transform.position.x;
			targetPos.x = temp;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		}

		if(otherObj != null) {
			otherObj.transform.position = transform.position + offsetToObj;
		}

	}

	void OnTriggerStay2D(Collider2D other){
		
		otherObj = other.gameObject;
		offsetToObj = otherObj.transform.position - transform.position;

	}

	void OnTriggerExit2D(Collider2D other){
		
		otherObj = null;

	}

}
