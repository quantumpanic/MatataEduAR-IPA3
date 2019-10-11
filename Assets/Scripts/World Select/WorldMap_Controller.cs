using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMap_Controller : MonoBehaviour {

	public string prog1;
	public string prog2;
	public string prog3;
	public string prog4;
	public string prog6;
	public string perpus;

	public void Bab1(){
        GlobalGameManager.Instance.currentArea = prog1;
		SceneManager.LoadScene (prog1);
	}

	public void Bab2()
    {
        GlobalGameManager.Instance.currentArea = prog2;
        SceneManager.LoadScene (prog2);
	}

	public void Bab3()
    {
        GlobalGameManager.Instance.currentArea = prog3;
        SceneManager.LoadScene (prog3);
	}

	public void Bab4()
    {
        GlobalGameManager.Instance.currentArea = prog4;
        SceneManager.LoadScene (prog4);
	}

	public void Bab6()
    {
        GlobalGameManager.Instance.currentArea = prog6;
        SceneManager.LoadScene (prog6);
	}

	public void Perpus()
    {
        GlobalGameManager.Instance.currentArea = perpus;
        SceneManager.LoadScene (perpus);
	}

}
