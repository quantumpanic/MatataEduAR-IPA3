using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCutscene : MonoBehaviour {

    public List<GameObject> frames = new List<GameObject>();
    public List<float> cueTimes = new List<float>();

    // Use this for initialization
    void Start() {

        HideAllFrames();

        foreach (float time in cueTimes)
        {
            Invoke("NextFrame", time);
        }

        Invoke("ShowRooster", 9);
    }
	
    void ShowRooster()
    {
        frames[frames.Count - 1].SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}

    int nextFrame;

    void NextFrame()
    {
        if (frames[nextFrame]) frames[nextFrame].SetActive(true);
        if (nextFrame > 0) if (frames[nextFrame - 1]) frames[nextFrame - 1].SetActive(false);
        nextFrame++;
    }

    void HideAllFrames()
    {
        foreach (GameObject g in frames)
        {
            if (g) g.SetActive(false);
        }
    }
}
