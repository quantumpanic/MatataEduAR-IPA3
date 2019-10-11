using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bab4_ProgressBar : MonoBehaviour {

	[Header("Reference To Another Object")]
	public GameObject player;
	public Image bar;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");

	}

	// Update is called once per frame
	void Update () {

		bar.fillAmount = player.GetComponent<Bab4_Player> ().curScore / player.GetComponent<Bab4_Player> ().score;

	}

}
