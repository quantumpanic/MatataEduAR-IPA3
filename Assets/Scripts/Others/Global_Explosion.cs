using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Explosion : MonoBehaviour {

	void Start(){

		StartCoroutine (Waiting());

	}

	IEnumerator Waiting(){
		
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);

	}

}
