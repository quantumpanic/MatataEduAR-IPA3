using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_HealthManager : MonoBehaviour {

	[Header("Status")]
	public int lifeAmmount;

	[Header("Additional Status")]
	public bool bg;

	[Header("Reference To Another Object")]
	List<GameObject> lifes;
	public GameObject life;

	[Header("Reference To Another Script")]
	public Bab6_Player player;

	// Use this for initialization
	void Start () {

		lifeAmmount = player.hitPoint;
		lifes = new List<GameObject>();
		GameObject temp;
		for(int i = 0; i < player.hitPoint; i++){
			temp = Instantiate (life);
			lifes.Add (temp);
			temp.transform.SetParent (transform);
//			temp.transform.parent = transform;
			temp.gameObject.SetActive (true);
		}

	}

	void Update(){

		if(!bg){
			if (lifeAmmount != player.hitPoint) {
				lifeAmmount = player.hitPoint;
				for(int i = 0; i < lifes.Count; i++){
					GameObject temp = lifes [i];
					lifes [i].SetActive (false);
					lifes.Remove (temp);
					break;
				}
			}
		}

	}
		
//			GameObject temp;
//			for(int i = 0; i < player.hitPoint; i++){
//				lifes.Remove (temp);
//				temp.gameObject.SetActive (false);
//			}
//		}
//
//	}

//	public GameObject three;
//	public GameObject two;
//	public GameObject one;
//	public Bab6_Player player;
//
//	void Start() {
//		
//		GameObject temp = GameObject.FindGameObjectWithTag ("Player");
//		player = temp.GetComponent<Bab6_Player>();
//
//	}
//
//	// Update is called once per frame
//	void Update () {
//		
//		if(player.hitPoint == 3){
//			three.SetActive (true);
//			two.SetActive (true);
//			one.SetActive (true);
//		}
//		else if(player.hitPoint == 2){
//			three.SetActive (false);
//			two.SetActive (true);
//			one.SetActive (true);
//		}
//		else if (player.hitPoint == 1) {
//			three.SetActive (false);
//			two.SetActive (false);
//			one.SetActive (true);
//		} else {
//			three.SetActive (false);
//			two.SetActive (false);
//			one.SetActive (false);
//		}
//
//	}
}
