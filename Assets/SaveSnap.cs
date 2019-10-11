using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSnap : MonoBehaviour {

	public static SaveSnap instance;

	public Global_WorldSelect world;
	public RectTransform rct; 

	void Awake(){

		DontDestroyOnLoad (gameObject);

		if(!instance){
			instance = this;
		}

	}

	void Update(){

		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("World Select")){
			if(world == null){
				world = GameObject.FindGameObjectWithTag ("snap").GetComponent<Global_WorldSelect>();
			}

			if(world != null){
				rct.anchoredPosition = world.panel.anchoredPosition;
			}
		}

	}

}
