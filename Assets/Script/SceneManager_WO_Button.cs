using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_WO_Button : MonoBehaviour {

	public string NextPage;

	// Use this for initialization
	void Start () {
		StartCoroutine (WaitingToLoad ());
	}

	IEnumerator WaitingToLoad()
	{
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene(NextPage);
	}

}
