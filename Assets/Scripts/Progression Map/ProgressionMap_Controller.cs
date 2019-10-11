using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionMap_Controller : MonoBehaviour {

	public string stage1;
	public string stage2;
	public string stage3;
	public string stage4;
	public string stage5;

	public void Stage1(){
		SceneManager.LoadScene (stage1);
	}

	public void Stage2(){
		SceneManager.LoadScene (stage2);
	}

	public void Stage3(){
		SceneManager.LoadScene (stage3);
	}

	public void Stage4(){
		SceneManager.LoadScene (stage4);
	}

	public void Stage5(){
		SceneManager.LoadScene (stage5);
	}

	public void Back(){
		SceneManager.LoadScene ("World Select");
	}

}
