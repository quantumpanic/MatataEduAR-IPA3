using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSwim : GameManagerClass {

	// Use this for initialization
	new void Start ()
    {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().StopAllCoroutines();
        gameData = DataHolder.Instance.curGameData;
        Invoke("PlayStarsAnime", 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Animator anim;

    void PlayStarsAnime()
    {
        Victory();
        return;

        anim.transform.parent.gameObject.SetActive(true);

        // add accumulated stars
        if (DataHolder.Instance.knowHolder == true)
        {
            DataHolder.Instance.totalKnowStars++;
        }
        else
        {
            GameObject.Find("ButtonAR").SetActive(false);
        }

        anim.SetInteger("Stars", DataHolder.Instance.starsHolder);
        anim.SetBool("Knowledge", DataHolder.Instance.knowHolder);
        
        Invoke("StarReminder", 2);

        anim.Play("Star0");

        // play music
        //GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMusic("in game_music2_win");
    }

    void StarReminder()
    {
        DataHolder.Instance.SetStarReminder(DataHolder.Instance.starsHolder + (DataHolder.Instance.knowHolder ? 1 : 0));
        DataHolder.Instance.ShowStarReminder();
    }
}
