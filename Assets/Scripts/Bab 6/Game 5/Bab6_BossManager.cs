using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_BossManager : MonoBehaviour {

	[Header("Status")]
	public bool sirus;
	public bool stratus;
	public bool nimbus;

	[Header("Reference To Another Object")]
	public GameObject sirus_obj;
	public GameObject stratus_obj;
	public GameObject nimbus_obj;
	public GameObject stratusPlate;
	public GameObject nimbusPlate;

	// Use this for initialization
	void Start () {

//		sirusPlate.SetActive (false);
		stratusPlate.SetActive (false);
		nimbusPlate.SetActive (false);

		sirus = true;
		stratus = false;
		nimbus = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (sirus) {
//			sirusPlate.SetActive (true);
			stratus_obj.SetActive (false);
			nimbus_obj.SetActive (false);
			sirus_obj.SetActive (true);
		} else if (stratus) {
			stratusPlate.SetActive (true);
			sirus_obj.SetActive (false);
			nimbus_obj.SetActive (false);
			stratus_obj.SetActive (true);
		} else if (nimbus) {
			nimbusPlate.SetActive (true);
			sirus_obj.SetActive (false);
			stratus_obj.SetActive (false);
			nimbus_obj.SetActive (true);
		} else {
			sirus_obj.SetActive (false);
			stratus_obj.SetActive (false);
			nimbus_obj.SetActive (false);
		}

	}
}
