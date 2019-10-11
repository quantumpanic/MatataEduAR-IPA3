using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab3_Controller_Guide : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject guide;

	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE
		if(!Input.anyKey){
			guide.gameObject.SetActive (true);
		}else
			guide.gameObject.SetActive (false);
		#endif

		#if UNITY_ANDROID || UNITY_IOS
		if (Input.touchCount == 0) {
			guide.gameObject.SetActive (true);
		} else
			guide.gameObject.SetActive (false);
		#endif
	}

}
