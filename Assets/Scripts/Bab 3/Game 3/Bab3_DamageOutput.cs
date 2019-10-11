using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bab3_DamageOutput : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject textObj;
	public Text textNumber;

	void Start(){

		textObj.SetActive (false);

	}

	public void ShowNumber(int dmg){

		textObj.SetActive (true);
		textNumber.text = "-" + dmg + "%";
		StartCoroutine (WaitAnimation());

	}

	IEnumerator WaitAnimation(){

		yield return new WaitForSeconds (0.5f);
		textObj.SetActive (false);

	}

}
