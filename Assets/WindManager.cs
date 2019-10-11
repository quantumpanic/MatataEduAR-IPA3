using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour {

    public float windStrength = 3; // percentage that the wind will keep turbines turning
    public Text windLbl;

	// Use this for initialization
	void Start ()
    {
        windLbl = GetComponentInChildren<Text>();
        InvokeRepeating("ChangeWindStr", 0, 10);
        InvokeRepeating("RandomizeWind", 0, .5f);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void ChangeWindStr()
    {
        windStrFixed += Random.Range(-1f, 1f);
        windStrFixed = Mathf.Clamp(windStrFixed, .5f, 2.5f);
    }

    float windStrFixed;

    void RandomizeWind()
    {
        windStrength = windStrFixed;// + Random.Range(-.5f, .5f);
        windLbl.text = "Wind" + '\n' + (windStrength*100f).ToString("F0") + "%";
    }
}
