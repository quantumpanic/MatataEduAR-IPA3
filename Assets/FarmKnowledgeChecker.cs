using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmKnowledgeChecker : MonoBehaviour {

    public GameObject cow1;
    public GameObject cow2;
    public GameObject horse1;
    public GameObject horse2;
    public GameObject wolf1;
    public GameObject wolf2;

    int stars;

    // Use this for initialization
    void Start () {


        // get stars
        stars = DataHolder.Instance.totalKnowStars;
        if (stars > 3) stars = 3;

        // show the critters based on stars
		switch (stars) {
		case 0:
			// set all inactive
			cow1.SetActive (false);
			cow2.SetActive (false);
			horse1.SetActive (false);
			horse2.SetActive (false);
			wolf1.SetActive (false);
			wolf2.SetActive (false);
			break;
		case 1:
			horse1.SetActive (false);
			horse2.SetActive (false);
			wolf1.SetActive (false);
			wolf2.SetActive (false);
			break;
		case 2:
			wolf1.SetActive (false);
			wolf2.SetActive (false);
			break;
		case 3:
			break;
		}
	}

    public void NextScene()
    {
        string nextScene = DataHolder.Instance.nextScene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
