using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_Wind : MonoBehaviour {

	[Header("Status")]
	public float speed;
	public float lifetime;
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.left * Time.deltaTime * speed);

		lifetime -= Time.deltaTime;

		if(lifetime <= 0f){
			Destroy (gameObject);
		}

	}

}
