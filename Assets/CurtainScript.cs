using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        nextScene = DataHolder.Instance.nextScene;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string nextScene;

    public void LoadNextScene()
    {
        Invoke("AsyncLoadScene", 1);
    }

    void AsyncLoadScene()
    {
        AsyncOperation old = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(0));

        // load next scene and unload previous scene
        AsyncOperation scn = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

        // callback for the level manager because this is an asynchronously loaded level
        GameObject.Find("LevelManager").GetComponent<PopupMenu>().AsyncLevelLoaded(SceneManager.GetSceneByName(nextScene).buildIndex);

        // add a marker object as a target to destroy the old scene
        var g = new GameObject("OLD");

        StartCoroutine(LoadingNextScene(scn));
    }

    IEnumerator LoadingNextScene(AsyncOperation scn)
    {
        while (!scn.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

        // scene done loading, play curtain anim
        AnimOpenCurtain();
    }

    public void AnimOpenCurtain()
    {
        GetComponent<Animator>().Play("CurtainsAnim2");
    }

    public void UnloadScene()
    {
        //Destroy(GameObject.Find("TRANSIT"));
        SceneManager.UnloadSceneAsync(GameObject.Find("TRANSIT").scene);
        SceneManager.UnloadSceneAsync(GameObject.Find("OLD").scene);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //foreach (GameObject g in GameObject.Find("OLD").scene.GetRootGameObjects())
        //{
        //    Destroy(g);
        //}
    }
}
