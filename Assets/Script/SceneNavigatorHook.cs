using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneNavigatorHook : MonoBehaviour {

    public Text mainTitle;
    public Button button;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
