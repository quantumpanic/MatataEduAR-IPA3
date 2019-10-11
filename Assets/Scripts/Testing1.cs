using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing1 : MonoBehaviour {

	public static Testing1 instance;
	public float nilai;

	void Awake(){
		if(!instance){
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
