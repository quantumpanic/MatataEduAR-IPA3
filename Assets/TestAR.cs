using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestAR : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void LoadChickenScene()
    {
        SceneManager.LoadScene("Chicken_AR");
    }

    public void LoadCowScene()
    {
        SceneManager.LoadScene("Farm_AR");
    }

    public void LoadPlantScene()
    {
        SceneManager.LoadScene("Tumbuhan_AR");
    }
}
