using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarGauge : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //gauge = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Image gauge;
    public List<Sprite> starSprites = new List<Sprite>();
    public GameObject knowStar;
    
    void ShowGauge(bool show = true)
    {
        //gauge.enabled = show;
        gauge.color = show ? Color.white : new Color(1, 1, 1, 0);
        knowStar.SetActive(show);
    }

    void UpdateGauge(GameUnlockedData gd)
    {
        int curStars = gd.stars;
        
        switch (curStars)
        {
            case 0:
                gauge.sprite = starSprites[0];
                break;
            case 1:
                gauge.sprite = starSprites[1];
                break;
            case 2:
                gauge.sprite = starSprites[2];
                break;
            case 3:
                gauge.sprite = starSprites[3];
                break;
        }

        knowStar.SetActive(gd.knowledgeStar ? true : false);
    }

    private void OnLevelWasLoaded(int level)
    {
        // check if this is a game scene for showing stars gauge
        bool isGame = false;
        var gameName = SceneManager.GetSceneByBuildIndex(level).name;
        GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o.name == gameName);

        if (gd != null) isGame = true;

        if (isGame)
        {
            ShowGauge(true);

            // add the values
            UpdateGauge(gd);
        }

        else
        {
            // if this is not a game scene, hide the gauge
            ShowGauge(false);
        }
    }
}
