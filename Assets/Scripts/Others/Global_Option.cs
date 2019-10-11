using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global_Option : MonoBehaviour {

	void Start(){

		Time.timeScale = 0;

	}

	public void LevelSelect(){
		
		Time.timeScale = 1;
		SceneManager.LoadScene ("World Select");

	}

	public void Continue(){
		
		Time.timeScale = 1;
		Destroy (gameObject);

	}

}
