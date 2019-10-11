using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour {

    public Image black;

	// Use this for initialization
	void Start () {
        black.CrossFadeAlpha(0, 1, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
