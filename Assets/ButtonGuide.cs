using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour {

	public Bab3_Player player;
	public Image atas, bawah;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(player.gas){
			Color a = atas.color;
			Color b = bawah.color;
			a.a = 1f;
			b.a = 1f;
			atas.color = a;
			bawah.color = b;
		}else{
			Color a = atas.color;
			Color b = bawah.color;
			a.a = .3f;
			b.a = .3f;
			atas.color = a;
			bawah.color = b;
		}

	}
}
