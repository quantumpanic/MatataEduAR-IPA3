using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Rotate : MonoBehaviour {

	[Header("Status")]
	public float rotSpeed;

	[Header("Additional Status")]
	public bool toRight;

	// Update is called once per frame
	void Update () {

		if(toRight){
			transform.Rotate (0, 0, rotSpeed * Time.deltaTime);
		}
		else
			transform.Rotate (0, 0, -rotSpeed * Time.deltaTime);

	}

}
