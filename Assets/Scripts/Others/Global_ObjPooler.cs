using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_ObjPooler : MonoBehaviour {

	[Header("Status")]
	public int bulletAmmount;

	[Header("Reference To Another Object")]
	public List<GameObject> bullets;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		bullets = new List<GameObject> ();
		for (int i = 0; i < bulletAmmount; i++){
			GameObject obj = (GameObject)Instantiate (bullet);
			obj.SetActive (false);
			bullets.Add (obj);
		}
	
	}
	
	public GameObject GetBullet(){
	
		for(int i = 0; i < bullets.Count; i++){
			if(!bullets[i].activeInHierarchy){
				return bullets[i];
			}
		}
//		GameObject obj = (GameObject)Instantiate (bullet);
//		obj.SetActive (false);
//		bullets.Add (obj);
//		return obj;
		return null;

	}

}
